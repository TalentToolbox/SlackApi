namespace SlackBlocks.Constants
{
    public static class ButtonStyles
    {
        public const string Primary = "primary";
        public const string Danger = "danger";
    }

    public static class BlockTypes
    {
        public const string Section = "section";
        public const string Divider = "divider";
        public const string Actions = "actions";
        public const string Context = "context";
        public const string Image = "image";
        public const string Header = "header";
    }

    public static class TextTypes
    {
        public const string Markdown = "mrkdwn";
        public const string PlainText = "plain_text";
    }

    public static class ElementTypes
    {
        public const string Image = "image";
        public const string Button = "button";
        public const string StaticSelect = "static_select";
        public const string ExternalSelect = "external_select";
        public const string UserSelect = "users_select";
        public const string ChannelSelect = "channel_select";
        public const string ConversationSelect = "conversation_select";
        public const string Overflow = "overflow";
        public const string DatePicker = "datepicker";
    }

    public static class ActionTypes
    {
        public const string Button = ElementTypes.Button;
        public const string StaticSelect = ElementTypes.StaticSelect;
        public const string UserSelect = ElementTypes.UserSelect;
        public const string DatePicker = ElementTypes.DatePicker;
        public const string RadioButtons = "radio_buttons";
        public const string PlainTextInput = "plain_text_input";
        public const string Checkboxes = "checkboxes";
        public const string MultiUsersSelect = "multi_users_select";
    }
}
