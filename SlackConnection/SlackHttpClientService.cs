﻿using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SlackConnection.DTO;
using SlackConnection.Interfaces;
using SlackConnection.Responses;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SlackConnection
{
    public class SlackHttpClientService : ISlackHttpClientService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly AuthenticationHeaderValue _authenticationHeaderValue;
        private readonly string SlackClientName;

        public SlackHttpClientService(IHttpClientFactory httpClientFactory, IConfigurationRoot config)
        {
            // Factory prevents socket exhastion https://aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/
            // and ensure DNS changes are respected https://byterot.blogspot.com/2016/07/singleton-httpclient-dns.html
            _httpClientFactory = httpClientFactory;

            var slackToken = config["Slack:AuthToken"];
            _authenticationHeaderValue = new AuthenticationHeaderValue("Bearer", slackToken);
            SlackClientName = config["Slack:ClientName"];
        }

        public async Task<List<User>> GetUsersAsync()
        {
            // Create Request Message
            //	https://slack.com/api/users.list
            var request = CreateRequestMessage(HttpMethod.Get, "api/users.list");

            var content = await SendRequestAsync(request);

            // Deserialize the JSON
            var response = JsonConvert.DeserializeObject<UserListResponse>(content);
            return response.Members;
        }

        public async Task<User> GetUserInfoAsync(string id)
        {
            var request = CreateRequestMessage(HttpMethod.Get, $"api/users.info?user={id}");
            var content = await SendRequestAsync(request);

            // Deserialize the JSON
            var response = JsonConvert.DeserializeObject<UserInfoResponse>(content);
            return response.User;
        }


        public async Task PublishViewAsync(string viewString)
        {
            try
            {
                var request = CreateRequestMessage(HttpMethod.Post, "api/views.publish");
                request.Content = EncodeJsonPostRequestContent(viewString);

                await SendRequestAsync(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private StringContent EncodeJsonPostRequestContent(string json)
        {
            return new StringContent(json, Encoding.UTF8, "application/json");
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
