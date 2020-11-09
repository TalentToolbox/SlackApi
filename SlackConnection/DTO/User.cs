using Newtonsoft.Json;
using System;

namespace SlackConnection.DTO
{
    public class User
    {
        public bool IsSlackBot
        {
            get
            {
                return Id.Equals("USLACKBOT", StringComparison.CurrentCultureIgnoreCase);
            }
        }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "team_id")]
        public string TeamId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "real_name")]
        public string RealName { get; set; }

        [JsonProperty(PropertyName = "profile")]
        public Profile Profile { get; set; }

        [JsonProperty(PropertyName = "deleted")]
        public bool Deleted { get; set; }
        [JsonProperty(PropertyName = "is_admin")]
        public bool IsAdmin { get; set; }
        [JsonProperty(PropertyName = "is_owner")]
        public bool IsOwner { get; set; }
        [JsonProperty(PropertyName = "is_primary_owner")]
        public bool IsPrimaryOwner { get; set; }
        [JsonProperty(PropertyName = "is_restricted")]
        public bool IsRestricted { get; set; }
        [JsonProperty(PropertyName = "is_ultra_restricted")]
        public bool IsUltraRestricted { get; set; }
        [JsonProperty(PropertyName = "is_bot")]
        public bool IsBot { get; set; }
        [JsonProperty(PropertyName = "is_app_user")]
        public bool IsAppUser { get; set; }
        [JsonProperty(PropertyName = "updated")]
        public int Updated { get; set; }
    }
}
