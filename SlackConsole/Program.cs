using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SlackConnection.Interfaces;
using System;
using System.Linq;

namespace SlackConsole
{
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
                var users = service.GetUsersAsync().Result;

                foreach (var user in users.Where(u => !u.IsSlackBot))
                {
                    Console.WriteLine(user.RealName);
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
