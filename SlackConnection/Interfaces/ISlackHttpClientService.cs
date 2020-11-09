using SlackConnection.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SlackConnection.Interfaces
{
    public interface ISlackHttpClientService
    {
        Task<List<Member>> GetUsersAsync();
        Task<Member> GetUserAsync(string id);
    }
}
