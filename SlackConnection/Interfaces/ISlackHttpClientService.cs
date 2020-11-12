using SlackConnection.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SlackConnection.Interfaces
{
    public interface ISlackHttpClientService
    {
        Task<List<User>> GetUsersAsync();
        Task<User> GetUserInfoAsync(string id);
        Task PublishViewAsync(string viewString);
    }
}
