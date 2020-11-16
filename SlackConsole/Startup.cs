﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SlackBlocks;
using SlackBlocks.Interfaces;
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
            services.AddTransient<IBlockService, BlockService>();
            services.AddSingleton(Configuration);

            var slackBaseUrl = Configuration["Slack:BaseUrl"];
            var slackClientName = Configuration["Slack:ClientName"];

            services.AddHttpClient(slackClientName, client =>
            {
                client.BaseAddress = new Uri(slackBaseUrl);
                client.Timeout = new TimeSpan(0, 0, 30);
                client.DefaultRequestHeaders.Clear();
            });
        }
    }
}
