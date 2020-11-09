using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SlackConnection.Interfaces;
using System;

namespace SlackConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var services = new ServiceCollection().AddLogging();

            var startup = new Startup();
            startup.ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();

            //configure console logging
            serviceProvider
                .GetService<ILoggerFactory>();

            var service = serviceProvider.GetService<ISlackHttpClientService>();
            var users = service.GetUsersAsync();

            Console.WriteLine("Hello World!");
        }
    }
}
