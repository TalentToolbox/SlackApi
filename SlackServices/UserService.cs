using SlackConnection.DTO;
using SlackConnection.Interfaces;
using SlackServices.Entity;
using SlackServices.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SlackServices
{
    public class UserService : IUserService
    {
        private readonly ISlackHttpClientService _clientService;

        public UserService(ISlackHttpClientService clientService)
        {
            _clientService = clientService;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _clientService.GetUsersAsync();
        }

        public async Task<DatabaseUser> SyncUser(User user)
        {
            var dbUser = new DatabaseUser
            {
                SlackUserId = user.Id,
                EmailAddress = user.Profile.Email,
                FirstName = user.Profile.FirstName,
                LastName = user.Profile.LastName,
                JobTitle = user.Profile.Title
            };

            // upsert data from slack user to our database
            // await UpserToDatabase(dbUser);

            return dbUser;
        }
    }
}
