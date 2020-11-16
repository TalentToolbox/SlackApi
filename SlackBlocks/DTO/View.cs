using Newtonsoft.Json;
using SlackBlocks.JsonConverters;

namespace SlackBlocks.DTO
{
    public class View
    {
        public string type { get; set; } = "home";

        [JsonConverter(typeof(BlockArrayConverter))]
        public IBlock[] blocks { get; set; }
    }
}
