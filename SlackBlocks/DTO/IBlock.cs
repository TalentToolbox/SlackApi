namespace SlackBlocks.DTO
{
    public interface IElement 
    {
        public string type { get; }
    }

    public interface IBlock 
    {
        public string type { get; }
        public string block_id { get; set; }
    }
}
