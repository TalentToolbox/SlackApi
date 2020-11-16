using Newtonsoft.Json;
using SlackBlocks.DTO;
using SlackBlocks.Interfaces;
using SlackConnection.Interfaces;
using System.Threading.Tasks;

namespace SlackBlocks
{
    public class PublishService : IPublishService
    {
        private ISlackHttpClientService _slackClient;

        public PublishService(ISlackHttpClientService slackClient)
        {
            _slackClient = slackClient;
        }

        public async Task PublishHomePageAsync(PublishRequest publishRequest)
        {
            var json = JsonConvert.SerializeObject(publishRequest,
                            Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });

            await _slackClient.PublishViewAsync(json);
        }
    }
}
