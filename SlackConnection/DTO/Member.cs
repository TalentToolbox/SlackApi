namespace SlackConnection.DTO
{
    public class Member
    {
        //"id": "USLACKBOT",
        //    "team_id": "T01DH8RPVD3",
        //    "name": "slackbot",
        //    "deleted": false,
        //    "color": "757575",
        //    "real_name": "Slackbot",
        //    "tz": "America/Los_Angeles",
        //    "tz_label": "Pacific Standard Time",
        //    "tz_offset": -28800,
        //    "profile": {

        //    },
        //    "is_admin": false,
        //    "is_owner": false,
        //    "is_primary_owner": false,
        //    "is_restricted": false,
        //    "is_ultra_restricted": false,
        //    "is_bot": false,
        //    "is_app_user": false,
        //    "updated": 0

        public string Id { get; set; }
        public string TeamId { get; set; }
        public string Name { get; set; }
        public bool Deleted { get; set; }
        public string RealName { get; set; }
        public Profile Profile { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsOwner { get; set; }
        public bool IsPrimaryOwner { get; set; }
        public bool IsRestricted { get; set; }
        public bool IsUltraRestricted { get; set; }
        public bool IsBot { get; set; }
        public bool IsAppUser { get; set; }
        public int Updated { get; set; }
    }
}
