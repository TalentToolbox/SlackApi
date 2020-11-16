using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SlackAppInteraction.DTO;
using SlackBlocks.DTO;
using SlackBlocks.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;

[assembly: FunctionsStartup(typeof(SlackAppInteraction.Startup))]
namespace SlackAppInteraction
{
    public class InteractionHandler
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

        private readonly IPublishService _publishService;

        public InteractionHandler(IPublishService publishService)
        {
            _publishService = publishService;
        }

        // https://api.slack.com/interactivity/handling
        [FunctionName("InteractionHandler")]
        public async Task<IActionResult> Run(
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

                    await ProcessPayload(payload);
                }
                catch (Exception ex3)
                {

                }

                return new OkResult();
            }
            catch (Exception ex)
            {
                throw;
            }
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

        private async Task ProcessPayload(string payload)
        {
            // This should be passed off to a seperate service
            // I'd make a HTTP Request to another Azure Function, or add it to a service bus queue
            await Task.Run(() =>
            {
                var response = JsonConvert.DeserializeObject<HomeTabPayload>(payload);

                foreach (var action in response.actions)
                {
                    var type = GetActionType(action.type);
                    ProcessAction(type, response.view, action, response.user.Id);
                }
            });

        }

        private void ProcessAction(SlackActionType type, View view, Interaction action, string userId)
        {
            switch (type)
            {
                case SlackActionType.Select:
                    ProcessSelectAction(view, action, userId);
                    break;
                case SlackActionType.FilteredConversationSelect:
                    break;
                case SlackActionType.Button:
                    break;
                case SlackActionType.UserSelect:
                    break;
                case SlackActionType.DatePicker:
                    break;
                case SlackActionType.Checkboxes:
                    break;
                case SlackActionType.RadioButton:
                    break;
                case SlackActionType.TimePicker:
                    break;
                case SlackActionType.PlainTextInput:
                    break;
                default:
                    break;
            }
        }

        private void ProcessSelectAction(View view, Interaction action, string userId)
        {
            int.TryParse(action.selected_option.value, out int value);

            if (value == 0)
                RespondToHappinessSelection(view, action, userId, "Sorry to hear that!");

            if (value == 1)
                RespondToHappinessSelection(view, action, userId, "Cool.");

            if (value == 2)
                RespondToHappinessSelection(view, action, userId, "Fantastic!");
        }

        private void RespondToHappinessSelection(View view, Interaction action, string userId, string response)
        {
            var request = new PublishRequest
            {
                user_id = userId,
                view = view
            };

            _publishService.PublishHomePageAsync(request);
        }

        
    }
}
