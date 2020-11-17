using SlackBlocks.DTO;
using SlackBlocks.Interfaces;
using System.Collections.Generic;

namespace SlackBlocks
{
    public class BlockService : IBlockService
    {
        public IBlock[] BuildDefaultHomeTab()
        {
            var imageBlock = new ImageBlock("Purple Cubed", "https://pbs.twimg.com/profile_images/879273391416541184/xivQgR5v_400x400.jpg", "Purple Logo");
            var header = new HeaderBlock("Welcome to your home page");
            var description = new SectionBlock("This page shows you personalised information unique to you and collects your feedback");
            var header2 = new HeaderBlock("Your Happiness");

            var happySection = new SectionBlock("How happy are you?");

            var options = new List<Option>
            {
                new Option
                {
                    text = new Text
                    {
                        text = "Unhappy"
                    },
                    value = "0"
                },
                new Option
                {
                    text = new Text
                    {
                        text = "Average"
                    },
                    value = "1"
                },
                new Option
                {
                    text = new Text
                    {
                        text = "Happy"
                    },
                    value = "2"
                }
            };

            var select = new StaticSelectElement()
            {
                options = options.ToArray()
            };

            happySection.accessory = select;

            var homePageBlocks = new List<IBlock>
            {
                imageBlock,
                header,
                description,
                header2,
                happySection
            };

            return homePageBlocks.ToArray();
        }

        public IBlock CreateMessageBlock(string response)
        {
            return new SectionBlock
            {
                text = new Text
                {
                    text = response,
                    emoji = true
                }
            };
        }
    }
}
