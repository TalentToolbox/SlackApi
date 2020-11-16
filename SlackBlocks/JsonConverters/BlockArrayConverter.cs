using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SlackBlocks.Constants;
using SlackBlocks.DTO;
using System;
using System.Collections.Generic;

namespace SlackBlocks.JsonConverters
{
    public class BlockArrayConverter : JsonConverter
    {
        public override bool CanWrite => false;
        public override bool CanRead => true;
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(IBlock);
        }
        public override void WriteJson(JsonWriter writer,
            object value, JsonSerializer serializer)
        {
            throw new InvalidOperationException("Use default serialization.");
        }

        public override object ReadJson(JsonReader reader,
            Type objectType, 
            object existingValue,
            JsonSerializer serializer)
        {
            // Variables.
            var blocks = new List<IBlock>();
            var jsonArray = JArray.Load(reader);

            // Deserialize each block
            foreach (var item in jsonArray)
            {
                var jsonObject = item as JObject;
                var block = default(IBlock);
                var blockType = jsonObject["type"].Value<string>();

                switch (blockType)
                {
                    case BlockTypes.Actions:
                        block = new ActionsBlock();
                        break;
                    case BlockTypes.Section:
                        block = new SectionBlock();
                        break;
                    case BlockTypes.Divider:
                        block = new DividerBlock();
                        break;
                    case BlockTypes.Image:
                        block = new ImageBlock();
                        break;
                    case BlockTypes.Context:
                        block = new ContextBlock();
                        break;
                    case BlockTypes.Header:
                        block = new HeaderBlock();
                        break;
                }

                // Populate the block instance.
                serializer.Populate(jsonObject.CreateReader(), block);
                blocks.Add(block);
            }

            return blocks.ToArray();
        }
    }
}
