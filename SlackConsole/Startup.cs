using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SlackConnection;
using SlackConnection.Interfaces;
using System;

namespace SlackConsole
{
    public class Startup
    {
        IConfigurationRoot Configuration { get; }

        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ISlackHttpClientService, SlackHttpClientService>();

            services.AddHttpClient(Constants.SlackClientName, client =>
            {
                client.BaseAddress = new Uri("https://slack.com/");
                client.Timeout = new TimeSpan(0, 0, 30);
                client.DefaultRequestHeaders.Clear();
            });
        }
    }
}
