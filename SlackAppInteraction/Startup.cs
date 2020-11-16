using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SlackBlocks;
using SlackBlocks.Interfaces;
using SlackConnection;
using SlackConnection.Interfaces;
using System;

namespace SlackAppInteraction
{
    public class Startup : FunctionsStartup
    {
        public IConfiguration Configuration { get; private set; }

        public override void Configure(IFunctionsHostBuilder builder)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            ConfigureServices(builder.Services);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ISlackHttpClientService, SlackHttpClientService>();
            services.AddTransient<IBlockService, BlockService>();
            services.AddTransient<IPublishService, PublishService>();

            var slackBaseUrl = Environment.GetEnvironmentVariable("Slack:BaseUrl");
            var slackClientName = Environment.GetEnvironmentVariable("Slack:ClientName");

            var test = Environment.GetEnvironmentVariables();


            services.AddHttpClient(slackClientName, client =>
            {
                client.BaseAddress = new Uri(slackBaseUrl);
                client.Timeout = new TimeSpan(0, 0, 30);
                client.DefaultRequestHeaders.Clear();
            });
        }
    }
}
