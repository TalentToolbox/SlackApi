using JoinTeamWebhook;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SlackServices.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

[assembly: FunctionsStartup(typeof(Startup))]
namespace JoinTeamWebhook
{
    public class SyncNewMember
    {
        private readonly string _signingSecret;

        private readonly IRequestVerificationService _verificationService;

        public SyncNewMember(IRequestVerificationService verificationService)
        {
            _signingSecret = Environment.GetEnvironmentVariable("Slack:SigningSecret");

            _verificationService = verificationService;
        }

        // https://api.slack.com/events-api#receiving_events
        // https://api.slack.com/events/team_join
        [FunctionName("SyncNewMember")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest request,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(request.Body).ReadToEndAsync();

            // Validate request signature against secret stored in environment variables
            if (!_verificationService.IsVerifiedSlackRequest(request, requestBody, _signingSecret))
            {
                return new UnauthorizedResult();
            }

            dynamic data = JsonConvert.DeserializeObject(requestBody);
            
            // Slack verifies URLs are working for Events, respond to challenge if it's this request
            if (_verificationService.IsUrlVerificationRequest(data, out string challenge))
            {
                return new JsonResult(challenge);
            }

            // Queue request / Make HttpRequest to other function
            //QueueRequest();

            // Respond to events with a HTTP 200 OK as soon as you can.
            // Avoid actually processing and reacting to events within the same process.
            // Implement a queue to handle inbound events after they are received.

            return new OkResult();
        }
    }
}
