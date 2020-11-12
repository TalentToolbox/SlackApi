using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SlackAppInteraction.DTO;
using System.Net.Http;

namespace SlackAppInteraction
{
    public static class InteractionHandler
    {

        const string MyUserId = "U01DDK9BTQW";

        public enum SlackActionType
        {
            Select,
            FilteredConversationSelect,
            Button,
            UserSelect,
            DatePicker,
            Checkboxes,
            RadioButton,
            TimePicker,
            PlainTextInput
        }

        private static SlackActionType GetActionType(string actionType)
        {
            return actionType switch
            {
                "button" => SlackActionType.Button,
                "static_select" => SlackActionType.Select,
                "users_select" => SlackActionType.UserSelect,
                "datepicker" => SlackActionType.DatePicker,
                "radio_buttons" => SlackActionType.RadioButton,
                "plain_text_input" => SlackActionType.PlainTextInput,
                "checkboxes" => SlackActionType.Checkboxes,
                "multi_users_select" => SlackActionType.UserSelect,
                _ => throw new NotImplementedException("Enum option not implemented"),
            };
        }

        // https://api.slack.com/interactivity/handling
        [FunctionName("InteractionHandler")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestMessage req,
            ILogger log)
        {
            try
            {
                log.LogInformation("C# HTTP trigger function processed a request.");

                // Respond to events with a HTTP 200 OK as soon as you can.
                // Avoid actually processing and reacting to events within the same process.
                // Implement a queue to handle inbound events after they are received.

                try
                {
                    var formData = await req.Content.ReadAsFormDataAsync();
                    string payload = formData.Get("payload");

                    var response = JsonConvert.DeserializeObject<HomeTabPayload>(payload);

                    foreach (var action in response.actions)
                    {
                        var type = GetActionType(action.type);
                    }
                }
                catch(Exception ex3)
                {

                }



                // Response URL is to post block elements as messages, e.g. Slackbot DMs, it cannot be used to update Home Tab (and is not provided)
                // It can be used with a modal from the home tab where a user selects a channel or a private message conversation for the ResponseUrl to target with
                //var responseUrl = data.ResponseUrl;

                //if (!string.IsNullOrEmpty(responseUrl))
                //{
                //    // You can use a response_url by making an HTTP POST directly to the URL and including a message payload in the HTTP body.
                //    // {  "text": "Thanks for your request, we'll process it and get back to you." }
                //}


                //// Verify
                //// https://api.slack.com/authentication/verifying-requests-from-slack
                //var token = data?.token;

                return new OkResult();
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
