using System.Collections.Generic;

namespace SlackConnection.DTO
{
    public class Profile
    {
        //        "title": "",
        //        "phone": "",
        //        "skype": "",
        //        "real_name": "Slackbot",
        //        "real_name_normalized": "Slackbot",
        //        "display_name": "Slackbot",
        //        "display_name_normalized": "Slackbot",
        //        "fields": null,
        //        "status_text": "",
        //        "status_emoji": "",
        //        "status_expiration": 0,
        //        "avatar_hash": "sv41d8cd98f0",
        //        "always_active": true,
        //        "first_name": "slackbot",
        //        "last_name": "",
        //        "image_24": "https://a.slack-edge.com/80588/img/slackbot_24.png",
        //        "image_32": "https://a.slack-edge.com/80588/img/slackbot_32.png",
        //        "image_48": "https://a.slack-edge.com/80588/img/slackbot_48.png",
        //        "image_72": "https://a.slack-edge.com/80588/img/slackbot_72.png",
        //        "image_192": "https://a.slack-edge.com/80588/marketing/img/avatars/slackbot/avatar-slackbot.png",
        //        "image_512": "https://a.slack-edge.com/80588/img/slackbot_512.png",
        //        "status_text_canonical": "",
        //        "team": "T01DH8RPVD3"

        public string Title { get; set; }
        public string Phone { get; set; }
        public string Skype { get; set; }
        public string RealName { get; set; }
        public string RealNameNormalized { get; set; }
        public string DisplayName { get; set; }
        public string DisplayNameNormalized { get; set; }
        public IEnumerable<string> Fields { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Team { get; set; }
    }
}
