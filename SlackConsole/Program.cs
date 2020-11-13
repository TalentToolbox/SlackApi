using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SlackBlocks.DTO;
using SlackBlocks.Interfaces;
using SlackConnection.Interfaces;
using System;
using System.Linq;

namespace SlackConsole
{
    public class PublishRequest
    {
        public string user_id { get; set; }
        public View view { get; set; }
    }

    public class View
    {
        public string type { get; set; } = "home";
        public IBlock[] blocks { get; set; }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Hello World!");
                var services = new ServiceCollection().AddLogging();

                var startup = new Startup();
                startup.ConfigureServices(services);
                var serviceProvider = services.BuildServiceProvider();

                //configure console logging
                serviceProvider
                    .GetService<ILoggerFactory>();

                var service = serviceProvider.GetService<ISlackHttpClientService>();
                var blockService = serviceProvider.GetService<IBlockService>();

                //service.PublishViewAsync(ViewString).Wait();

                var users = service.GetUsersAsync().Result;

				// Sync user data
                foreach (var user in users.Where(u => !u.IsSlackBot))
                {
					// Publish dashboard for each user
					// This can be unique for each user, so should store state the database
					// Can get whatever unique data/images/messages to dispaly from the database
                    Console.WriteLine(user.RealName);
                    var homeBlocks = blockService.BuildDefaultHomeTab();
                    var publishRequest = new PublishRequest
                    {
                        user_id = user.Id,
                        view = new View
                        {
                            blocks = homeBlocks
                        }
                    };

                    var json = JsonConvert.SerializeObject(publishRequest);

                    service.PublishViewAsync(json).Wait();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");

                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner: {ex.InnerException.Message}");
                }
            }
        }
    }
}
