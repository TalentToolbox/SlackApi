using Newtonsoft.Json;
using SlackConnection.DTO;
using System.Collections.Generic;

namespace SlackConnection.Responses
{
    public class UserListResponse : Response
    {
        [JsonProperty(PropertyName = "members")]
        public List<User> Members;
    }
}
