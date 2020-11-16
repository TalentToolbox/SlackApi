using SlackBlocks.DTO;
using System.Threading.Tasks;

namespace SlackBlocks.Interfaces
{
    public interface IPublishService
    {
        Task PublishHomePageAsync(PublishRequest publishRequest);
    }
}
