using SlackConnection.Interfaces;
using System;

namespace SlackServices
{
    public class UserSyncService
    {
        private readonly ISlackHttpClientService _clientService;

        public UserSyncService(ISlackHttpClientService clientService)
        {
            _clientService = clientService;
        }
    }
}
