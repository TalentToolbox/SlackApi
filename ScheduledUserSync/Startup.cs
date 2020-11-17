using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SlackBlocks;
using SlackBlocks.Interfaces;
using SlackConnection;
using SlackConnection.Interfaces;
using SlackServices;
using SlackServices.Interfaces;
using System;

namespace ScheduledHomePageBuild
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
            services.AddTransient<IUserService, UserService>();

            var slackBaseUrl = Environment.GetEnvironmentVariable("Slack:BaseUrl");
            var slackClientName = Environment.GetEnvironmentVariable("Slack:ClientName");

            services.AddHttpClient(slackClientName, client =>
            {
                client.BaseAddress = new Uri(slackBaseUrl);
                client.Timeout = new TimeSpan(0, 0, 30);
                client.DefaultRequestHeaders.Clear();
            });
        }
    }
}
