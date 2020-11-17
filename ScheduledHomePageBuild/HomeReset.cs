using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using SlackBlocks.DTO;
using SlackBlocks.Interfaces;
using SlackServices.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

[assembly: FunctionsStartup(typeof(ScheduledHomePageBuild.Startup))]
namespace ScheduledHomePageBuild
{
    public class HomeReset
    {
        private readonly IPublishService _publishService;
        private readonly IBlockService _blockService;
        private readonly IUserService _userService;

        public HomeReset(IPublishService publishService, 
            IBlockService blockService,
            IUserService userService)
        {
            _publishService = publishService;
            _blockService = blockService;
            _userService = userService;
        }

        [FunctionName("HomeReset")]
        public async Task Run([TimerTrigger("0 */2 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            var users = await _userService.GetUsersAsync();

            foreach (var user in users.Where(u => !u.IsSlackBot && !u.IsBot))
            {
                // Publish dashboard for each user
                // This can be unique for each user, so should store state the database
                // Can get whatever unique data/images/messages to dispaly from the database

                var homeBlocks = _blockService.BuildDefaultHomeTab();
                var publishRequest = new PublishRequest
                {
                    user_id = user.Id,
                    view = new View
                    {
                        blocks = homeBlocks
                    }
                };

                await _publishService.PublishHomePageAsync(publishRequest);
            }

            log.LogInformation($"C# Timer trigger function finished at: {DateTime.Now}");
        }
    }
}
