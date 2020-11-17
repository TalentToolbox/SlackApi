using SlackBlocks.DTO;

namespace SlackBlocks.Interfaces
{
    public interface IBlockService
    {
        IBlock[] BuildDefaultHomeTab();
        IBlock CreateMessageBlock(string response);
    }
}
