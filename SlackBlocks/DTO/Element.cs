using SlackBlocks.Constants;

namespace SlackBlocks.DTO
{
    public class Text : IElement
    {
        public string type { get; set; } = TextTypes.PlainText;
        public string text { get; set; } = "";
        public bool? emoji { get; set; }
        // public bool? verbatim { get; set; }
    }

    public class Option
    {
        public Text text { get; set; }
        public string value { get; set; }
    }

    public class OptionGroups
    {
        public Text label { get; set; }
        public Option[] options { get; set; }
    }

    public class Confirm
    {
        public Text title { get; set; }
        public Text text { get; set; }
        public Text confirm { get; set; }
        public Text deny { get; set; }
    }

    public class Element : IElement
    {
        public string type { get; set; }
        public string action_id { get; set; }
        public Text text { get; set; }
        public string value { get; set; }

        public Text placeholder { get; set; }
        public Option[] options { get; set; }

        public OptionGroups[] option_groups { get; set; }
        public string image_url { get; set; }
        public string alt_text { get; set; } = string.Empty;
        public string url { get; set; }
        public string initial_date { get; set; }
        public string initial_user { get; set; }
        public string initial_channel { get; set; }
        public string initial_conversation { get; set; }
        public string initial_option { get; set; }
        public int? min_query_length { get; set; }
        public Confirm confirm { get; set; }
        public string style { get; set; }
    }
    public class ImageElement : IElement
    {
        public string type { get; } = ElementTypes.Image;
        public string image_url { get; set; }
        public string alt_text { get; set; } = string.Empty;
    }
    public class ButtonElement : IElement
    {
        public string type { get; } = ElementTypes.Button;
        public string action_id { get; set; }
        public Text text { get; set; }
        public string value { get; set; }

        public Text placeholder { get; set; }
        public Option[] options { get; set; }

        public OptionGroups[] option_groups { get; set; }
        public string url { get; set; }
        public Confirm confirm { get; set; }
        public string style { get; set; }
    }

    public class StaticSelectElement : IElement
    {
        public string type { get; } = ElementTypes.StaticSelect;
        public string action_id { get; set; }

        public Text placeholder { get; set; }
        public Option[] options { get; set; }

        public OptionGroups[] option_groups { get; set; }
        public string initial_option { get; set; }
        public Confirm confirm { get; set; }
    }
    public class ExternalSelectElement : IElement
    {
        public string type { get; } = ElementTypes.ExternalSelect;
        public string action_id { get; set; }

        public Text placeholder { get; set; }
        public string initial_option { get; set; }
        public int min_query_length { get; set; }
        public Confirm confirm { get; set; }
    }


    public class UserSelectElement : IElement
    {
        public string type { get; } = ElementTypes.UserSelect;
        public string action_id { get; set; }

        public Text placeholder { get; set; }
        public string initial_user { get; set; }
        public Confirm confirm { get; set; }
    }
    public class ConversationSelectElement : IElement
    {
        public string type { get; } = ElementTypes.ChannelSelect;
        public string action_id { get; set; }

        public Text placeholder { get; set; }
        public string initial_conversation { get; set; }
        public Confirm confirm { get; set; }
    }
    public class ChannelSelectElement : IElement
    {
        public string type { get; } = ElementTypes.ChannelSelect;
        public string action_id { get; set; }

        public Text placeholder { get; set; }
        public string initial_channel { get; set; }
        public Confirm confirm { get; set; }
    }
    public class OverflowElement : IElement
    {
        public string type { get; } = ElementTypes.Overflow;
        public string action_id { get; set; }
        public Option[] options { get; set; }
        public Confirm confirm { get; set; }
    }

    public class DatePickerElement : IElement
    {
        public string type { get; } = ElementTypes.DatePicker;
        public string action_id { get; set; }

        public Text placeholder { get; set; }
        public string initial_date { get; set; }
        public Confirm confirm { get; set; }
    }
}
