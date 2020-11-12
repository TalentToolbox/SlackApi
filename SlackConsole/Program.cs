using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SlackConnection.Interfaces;
using System;
using System.Linq;

namespace SlackConsole
{
    public class Program
    {
        const string ViewString = @"{
	""blocks"": [
		{
			""type"": ""actions"",
			""elements"": [
				{
					""type"": ""button"",
					""text"": {
						""type"": ""plain_text"",
						""emoji"": true,
						""text"": ""Add a suggestion""
					},
					""value"": ""click_me_123""
				}
			]
		},
		{
			""type"": ""divider""
		},
		{
			""type"": ""context"",
			""elements"": [
				{
					""type"": ""mrkdwn"",
					""text"": ""No votes""
				}
			]
		},
		{
			""type"": ""section"",
			""text"": {
				""type"": ""mrkdwn"",
				""text"": "":ramen: *Kagawa-Ya Udon Noodle Shop*\nDo you like to shop for noodles? We have noodles.""
			},
			""accessory"": {
				""type"": ""button"",
				""text"": {
					""type"": ""plain_text"",
					""emoji"": true,
					""text"": ""Vote""
				},
				""value"": ""click_me_123""
			}
		},
		{
			""type"": ""context"",
			""elements"": [
				{
					""type"": ""image"",
					""image_url"": ""https://api.slack.com/img/blocks/bkb_template_images/profile_4.png"",
					""alt_text"": ""Angela""
				},
				{
					""type"": ""image"",
					""image_url"": ""https://api.slack.com/img/blocks/bkb_template_images/profile_2.png"",
					""alt_text"": ""Dwight Schrute""
				},
				{
					""type"": ""plain_text"",
					""emoji"": true,
					""text"": ""2 votes""
				}
			]
		},
		{
			""type"": ""section"",
			""text"": {
				""type"": ""mrkdwn"",
				""text"": "":hamburger: *Super Hungryman Hamburgers*\nOnly for the hungriest of the hungry.""
			},
			""accessory"": {
				""type"": ""button"",
				""text"": {
					""type"": ""plain_text"",
					""emoji"": true,
					""text"": ""Vote""
				},
				""value"": ""click_me_123""
			}
		},
		{
			""type"": ""context"",
			""elements"": [
				{
					""type"": ""image"",
					""image_url"": ""https://api.slack.com/img/blocks/bkb_template_images/profile_1.png"",
					""alt_text"": ""Michael Scott""
				},
				{
					""type"": ""image"",
					""image_url"": ""https://api.slack.com/img/blocks/bkb_template_images/profile_2.png"",
					""alt_text"": ""Dwight Schrute""
				},
				{
					""type"": ""image"",
					""image_url"": ""https://api.slack.com/img/blocks/bkb_template_images/profile_3.png"",
					""alt_text"": ""Pam Beasely""
				},
				{
					""type"": ""plain_text"",
					""emoji"": true,
					""text"": ""3 votes""
				}
			]
		},
		{
			""type"": ""section"",
			""text"": {
				""type"": ""mrkdwn"",
				""text"": "":sushi: *Ace Wasabi Rock-n-Roll Sushi Bar*\nThe best landlocked sushi restaurant.""
			},
			""accessory"": {
				""type"": ""button"",
				""text"": {
					""type"": ""plain_text"",
					""emoji"": true,
					""text"": ""Vote""
				},
				""value"": ""click_me_123""
			}
		},
		{
			""type"": ""divider""
		},
		{
			""type"": ""section"",
			""text"": {
				""type"": ""mrkdwn"",
				""text"": ""*Where should we order lunch from?* Poll by <fakeLink.toUser.com|Mark>""
			}
		}
	]
}";

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

				service.PublishViewAsync(ViewString).Wait();

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
