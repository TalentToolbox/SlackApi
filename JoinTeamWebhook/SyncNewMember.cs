using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace JoinTeamWebhook
{
    public static class SyncNewMember
    {
        // https://jointeamwebhook.azurewebsites.net/api/SyncNewMember?code=uLX3DWojBHW4hvfCUkfNLjVRGWKvQaKt9jd0SnFG0VaTPQ5A1U5kUA==
        // https://api.slack.com/events-api#receiving_events
        // https://api.slack.com/events/team_join
        [FunctionName("SyncNewMember")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            // Respond to events with a HTTP 200 OK as soon as you can.
            // Avoid actually processing and reacting to events within the same process.
            // Implement a queue to handle inbound events after they are received.

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            //var user = data?.event?.user;

            // Verify - better to use signing secret than this token it seems this token value is deprecated
            // https://api.slack.com/authentication/verifying-requests-from-slack
            var token = data?.token;

            return new OkResult();
        }

        public class ChallengeResponse
        {
            public string challenge { get; set; }
        }
    }
}
