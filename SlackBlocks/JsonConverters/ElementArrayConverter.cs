﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SlackBlocks.Constants;
using SlackBlocks.DTO;
using System;
using System.Collections.Generic;

namespace SlackBlocks.JsonConverters
{
    public class ElementArrayConverter : JsonConverter
    {
        public override bool CanWrite => false;
        public override bool CanRead => true;
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(IElement);
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
            var elements = new List<IElement>();
            var jsonArray = JArray.Load(reader);

            // Deserialize each block
            foreach (var item in jsonArray)
            {
                var jsonObject = item as JObject;
                var element = default(IElement);
                var elementType = jsonObject["type"].Value<string>();

                switch (elementType)
                {
                    case ElementTypes.Button:
                        element = new ButtonElement();
                        break;
                    case ElementTypes.ChannelSelect:
                        element = new ChannelSelectElement();
                        break;
                    case ElementTypes.ConversationSelect:
                        element = new ConversationSelectElement();
                        break;
                    case ElementTypes.DatePicker:
                        element = new DatePickerElement();
                        break;
                    case ElementTypes.ExternalSelect:
                        element = new ExternalSelectElement();
                        break;
                    case ElementTypes.Image:
                        element = new ImageElement();
                        break;
                    case ElementTypes.Overflow:
                        element = new OverflowElement();
                        break;
                    case ElementTypes.StaticSelect:
                        element = new StaticSelectElement();
                        break;
                    case ElementTypes.UserSelect:
                        element = new UserSelectElement();
                        break;
                }

                // Populate the block instance.
                serializer.Populate(jsonObject.CreateReader(), element);
                elements.Add(element);
            }

            return elements.ToArray();
        }
    }
}
