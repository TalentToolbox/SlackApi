using SlackBlocks.DTO;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SlackAppInteraction.DTO
{
    public class HomeTabPayload
    {
        /// <summary>
        ///     block_actions payloads are received when a user clicks a Block Kit interactive component.
        ///     shortcut and message_actions payloads are received when global and message shortcuts are used.
        ///     view_submission payloads are received when a modal is submitted.
        ///     view_closed payloads are received when a modal is cancelled.
        /// </summary>
        public string type { get; set; }

        /// <summary>
        ///     The user who interacted to trigger this request.
        /// </summary>
        public User user { get; set; }

        public string api_app_id { get; set; }

        public string token { get; set; }

        public Container container { get; set; }

        /// <summary>
        /// 	A short-lived ID that can be used to open modals.
        /// 	Can also get a redirect URL with this to response with a message response to the user
        /// </summary>
        public string trigger_id { get; set; }

        public Team team { get; set; }

        ///// <summary>
        /////     A short-lived webhook that can be used to send messages in response to interactions.
        ///// </summary>
        //[JsonPropertyName("response_url")]
        //public string ResponseUrl { get; set; }

        /// <summary>
        /// Contains data from the specific interactive component that was used.
        /// App surfaces can contain blocks with multiple interactive components, and each of those components can have multiple values selected by users. 
        /// Combine the fields within actions (shown below) to help provide the full context of the interaction.
        /// </summary>
        public IEnumerable<Interaction> actions { get; set; }

        public View view { get; set; }
    }

    public class Container
    {
        public string type { get; set; }
        public string view_id { get; set; }
    }

    public class User
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("username")]
        public string Username { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("team_id")]
        public string TeamId { get; set; }
    }

    public class Interaction
    {
        public string type { get; set; }

        /// <summary>
        /// 	Identifies the interactive component itself. 
        /// 	Some blocks can contain multiple interactive components, so the block_id alone may not be specific enough to identify the source component. 
        /// 	See the reference guide for the interactive element you're using for more info on the action_id field.
        /// </summary>
        public string action_id { get; set; }

        /// <summary>
        ///     Identifies the block within a surface that contained the interactive component that was used. 
        ///     See the reference guide for the block you're using for more info on the block_id field.
        /// </summary>
        public string block_id { get; set; }

        public SelectedOption selected_option { get; set; }

        ///// <summary>
        /////     Set by your app when you composed the blocks, this is the value that was specified in the interactive component when an interaction happened. 
        /////     For example, a select menu will have multiple possible values depending on what the user picks from the menu, and value will identify the chosen option. 
        /////     See the reference guide for the interactive element you're using for more info on the value field.
        ///// </summary>
        //[JsonPropertyName("value")]
        //public string Value { get; set; }

        public string action_ts { get; set; }
    }

    public class SelectedOption
    {
        public Text text { get; set; }

        /// <summary>
        ///     Set by your app when you composed the blocks, this is the value that was specified in the interactive component when an interaction happened. 
        ///     For example, a select menu will have multiple possible values depending on what the user picks from the menu, and value will identify the chosen option. 
        ///     See the reference guide for the interactive element you're using for more info on the value field.
        /// </summary>
        public string value { get; set; }
    }

    public class Text
    {
        public string type { get; set; }
        public string text { get; set; }
        public bool emoji { get; set; }
    }

    public class Team
    {
        public string id { get; set; }

        public string domain { get; set; }
    }
}
