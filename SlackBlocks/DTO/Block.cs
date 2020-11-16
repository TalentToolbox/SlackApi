using Newtonsoft.Json;
using SlackBlocks.Constants;
using SlackBlocks.JsonConverters;
using System;

namespace SlackBlocks.DTO
{
    //see https://api.slack.com/reference/messaging/blocks
    public class Block : IBlock
    {
        public string type { get; set; }
        
        public string block_id { get; set; }
        public Text text { get; set; }
        public Element accessory { get; set; }
        public Element[] elements { get; set; }
        public Text title { get; set; }
        public string image_url { get; set; }
        public string alt_text { get; set; } = string.Empty;

        public Text[] fields { get; set; }
    }
    public class SectionBlock : IBlock
    {
        public SectionBlock() { }

        public SectionBlock(string textContent)
        {
            text = new Text
            {
                type = TextTypes.Markdown,
                text = textContent
            };
        }

        public string type { get; } = BlockTypes.Section;
        
        public string block_id { get; set; }
        public Text text { get; set; }

        [JsonConverter(typeof(ElementConverter))]
        public IElement accessory { get; set; }
        
        public Text[] fields { get; set; }
    }
    public class DividerBlock : IBlock
    {
        public string type { get; } = BlockTypes.Divider;
        
        public string block_id { get; set; }
    }
    public class ImageBlock : IBlock
    {
        public ImageBlock() {}

        public ImageBlock(string title, string imageUrl, string altText)
        {
            if (string.IsNullOrEmpty(altText))
                throw new InvalidOperationException("Slack API will not permit images without alt text");

            this.title = new Text
            {
                type = TextTypes.PlainText,
                text = title,
                emoji = true
            };

            image_url = imageUrl;
            alt_text = altText;
        }

        public string type { get; } = BlockTypes.Image;
        
        public string block_id { get; set; }
        public Text title { get; set; }
        public string image_url { get; set; }

        public string alt_text { get; set; } = string.Empty;
    }
    public class ActionsBlock : IBlock
    {
        public string type { get; } = BlockTypes.Actions;
        
        public string block_id { get; set; }
        [JsonConverter(typeof(ElementArrayConverter))]
        public IElement[] elements { get; set; }
    }
    public class ContextBlock : IBlock
    {
        public string type { get; } = BlockTypes.Context;

        
        public string block_id { get; set; }
        [JsonConverter(typeof(ElementArrayConverter))]
        public IElement[] elements { get; set; }
    }
    public class HeaderBlock : IBlock
    {
        public HeaderBlock(){}

        public HeaderBlock(string textContent)
        {
            text = new Text
            {
                type = TextTypes.PlainText,
                text = textContent
            };
        }

        public string type { get; } = BlockTypes.Header;
        public Text text { get; set; }
        
        public string block_id { get; set; }
    }
}
