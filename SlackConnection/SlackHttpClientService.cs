using Newtonsoft.Json;
using SlackConnection.DTO;
using SlackConnection.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SlackConnection
{
    public class SlackHttpClientService : ISlackHttpClientService
    {
        const string SlackClientName = "SlackClient";

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly AuthenticationHeaderValue _authenticationHeaderValue;

        public SlackHttpClientService(IHttpClientFactory httpClientFactory)
        {
            // Factory prevents socket exhastion https://aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/
            // and ensure DNS changes are respected https://byterot.blogspot.com/2016/07/singleton-httpclient-dns.html
            _httpClientFactory = httpClientFactory;

            var slackToken = Environment.GetEnvironmentVariable("Slack:AuthToken");
            _authenticationHeaderValue = new AuthenticationHeaderValue("Authorization", $"Bearer {slackToken}");
        }

        public async Task<List<Member>> GetUsersAsync()
        {
            // Create Request Message
            //	https://slack.com/api/users.list
            var request = CreateRequestMessage(HttpMethod.Get, "api/users.list");

            var content = await SendRequestAsync(request);

            // Deserialize the JSON
            var users = JsonConvert.DeserializeObject<List<Member>>(content);
            return users;
        }

        public async Task<Member> GetUserAsync(string id)
        {
            var request = CreateRequestMessage(HttpMethod.Get, $"api/users.info?user={id}");
            var content = await SendRequestAsync(request);

            // Deserialize the JSON
            var user = JsonConvert.DeserializeObject<Member>(content);
            return user;
        }

        private HttpRequestMessage CreateRequestMessage(HttpMethod method, string urlEndpoint)
        {
            var request = new HttpRequestMessage(method, urlEndpoint);
            request.Headers.Authorization = _authenticationHeaderValue;
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return request;
        }

        private async Task<string> SendRequestAsync(HttpRequestMessage request)
        {
            var httpClient = _httpClientFactory.CreateClient(SlackClientName);
            using var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            // Ensure we have a Success Status Code - throws exception if success is false
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return content;
        }
    }
}
