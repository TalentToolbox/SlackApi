using Newtonsoft.Json;
using SlackConnection.DTO;

namespace SlackConnection.Responses
{
    public class UserInfoResponse : Response
    {
        [JsonProperty(PropertyName = "user")]
        public User User;
    }
}
