using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace SlackAppInteraction
{
    public static class InteractionHandler
    {
        // https://api.slack.com/interactivity/handling
        [FunctionName("InteractionHandler")]
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


            var responseUrl = data.response_url;

            if (!string.IsNullOrEmpty(responseUrl))
            {
                // You can use a response_url by making an HTTP POST directly to the URL and including a message payload in the HTTP body.
                // {  "text": "Thanks for your request, we'll process it and get back to you." }
            }



            // Verify
            // https://api.slack.com/authentication/verifying-requests-from-slack
            var token = data?.token;

            return new OkResult();
        }
    }
}
