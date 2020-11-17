using SlackBlocks.Constants;
using System;

namespace SlackBlocks.Enum
{
    public static class EnumHelper
    {
        public static SlackActionType GetActionType(string actionType)
        {
            return actionType switch
            {
                ActionTypes.Button => SlackActionType.Button,
                ActionTypes.StaticSelect => SlackActionType.Select,
                ActionTypes.UserSelect => SlackActionType.UserSelect,
                ActionTypes.DatePicker => SlackActionType.DatePicker,
                ActionTypes.RadioButtons => SlackActionType.RadioButton,
                ActionTypes.PlainTextInput => SlackActionType.PlainTextInput,
                ActionTypes.Checkboxes => SlackActionType.Checkboxes,
                ActionTypes.MultiUsersSelect => SlackActionType.UserSelect,
                _ => throw new NotImplementedException("Enum option not implemented"),
            };
        }
    }
}
