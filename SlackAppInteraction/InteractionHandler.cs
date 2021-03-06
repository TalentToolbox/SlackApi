using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SlackAppInteraction.DTO;
using SlackBlocks.DTO;
using SlackBlocks.Enum;
using SlackBlocks.Interfaces;
using SlackServices.Interfaces;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

[assembly: FunctionsStartup(typeof(SlackAppInteraction.Startup))]
namespace SlackAppInteraction
{
    /// <summary>
    /// All interactions with the Slack App Homepage will pass through here
    /// The API the events are sent to is set from the App Configuration https://api.slack.com/events/team_join
    /// </summary>
    public class InteractionHandler
    {
        private readonly IPublishService _publishService;
        private readonly IBlockService _blockService;
        private readonly IRequestVerificationService _verificationService;

        private readonly string _signingSecret;

        public InteractionHandler(IPublishService publishService,
            IBlockService blockService,
            IRequestVerificationService verificationService)
        {
            _publishService = publishService;
            _blockService = blockService;
            _verificationService = verificationService;

            _signingSecret = Environment.GetEnvironmentVariable("Slack:SigningSecret");
        }

        // https://api.slack.com/interactivity/handling
        [FunctionName("InteractionHandler")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestMessage request,
            ILogger log)
        {
            try
            {
                log.LogInformation("C# HTTP trigger function processed a request.");

                string requestBody = await request.Content.ReadAsStringAsync();
                var requestTimestamp = request.Headers.GetValues("X-Slack-Request-Timestamp").FirstOrDefault();
                var slackSignature = request.Headers.GetValues("X-Slack-Signature").FirstOrDefault();

                // Validate request signature against secret stored in environment variables
                if (!_verificationService.IsVerifiedSlackRequest(requestTimestamp, requestBody, slackSignature, _signingSecret))
                {
                    return new UnauthorizedResult();
                }

                // Respond to events with a HTTP 200 OK as soon as you can.
                // Avoid actually processing and reacting to events within the same process.
                // Implement a queue to handle inbound events after they are received.

                var formData = await request.Content.ReadAsFormDataAsync();
                string payload = formData.Get("payload");

                await ProcessPayload(payload);

                return new OkResult();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // The below code would be in a seperate project and run by a Service Bus Trigger or HttpTrigger in production
        // This is so Slack can quickly get its HTTP 200 response

        private async Task ProcessPayload(string payload)
        {
            // This should be passed off to a seperate service
            // I'd make a HTTP Request to another Azure Function, or add it to a service bus queue
            await Task.Run(() =>
            {
                var response = JsonConvert.DeserializeObject<HomeTabPayload>(payload);

                foreach (var action in response.actions)
                {
                    var type = EnumHelper.GetActionType(action.type);
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
                RespondToHappinessSelection(view, action, userId, "Cool!");

            if (value == 2)
                RespondToHappinessSelection(view, action, userId, "Fantastic!");
        }

        private void RespondToHappinessSelection(View view, Interaction action, string userId, string response)
        {
            var actionBlock = view.blocks.Single(b => b.block_id == action.block_id);
            var actionBlockIndex = Array.IndexOf(view.blocks, actionBlock);

            view.blocks[actionBlockIndex] = _blockService.CreateMessageBlock($"{response} Thanks for sharing this with us :)");

            var request = new PublishRequest
            {
                user_id = userId,
                view = view
            };

            _publishService.PublishHomePageAsync(request);
        }
    }
}
