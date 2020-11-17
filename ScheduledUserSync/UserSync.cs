using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using SlackServices.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduledUserSync
{
    public class UserSync
    {
        private readonly IUserService _userService;

        public UserSync(IUserService userService)
        {
            _userService = userService;
        }

        [FunctionName("UserSync")]
        public async Task Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            var users = await _userService.GetUsersAsync();

            foreach (var user in users.Where(u => !u.IsSlackBot && !u.IsBot))
            {
                await _userService.SyncUser(user);
            }

            log.LogInformation($"C# Timer trigger function finished at: {DateTime.Now}");
        }
    }
}
