using SlackConnection.DTO;
using SlackServices.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SlackServices.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetUsersAsync();
        Task<DatabaseUser> SyncUser(User user);
    }
}
