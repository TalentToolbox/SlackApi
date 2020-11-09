using Newtonsoft.Json;

namespace SlackConnection.DTO
{
    public class Profile
    {
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "phone")]
        public string Phone { get; set; }
        [JsonProperty(PropertyName = "skype")]
        public string Skype { get; set; }
        [JsonProperty(PropertyName = "real_name")]
        public string RealName { get; set; }
        [JsonProperty(PropertyName = "real_name_normalized")]
        public string RealNameNormalized { get; set; }
        [JsonProperty(PropertyName = "display_name")]
        public string DisplayName { get; set; }
        [JsonProperty(PropertyName = "display_name_normalized")]
        public string DisplayNameNormalized { get; set; }
        //[JsonProperty(PropertyName = "fields")]
        //public IEnumerable<string> Fields { get; set; }
        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; }
        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; set; }
        [JsonProperty(PropertyName = "team")]
        public string Team { get; set; }
    }
}
