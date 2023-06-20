using MediatR;

namespace RestArchitecture.Handlers.Items
{
    public class DeleteItemRequest : IRequest<bool>
    {
        public int ItemId { get; }
        public DeleteItemRequest(int itemId)
        {
            ItemId = itemId;
        }
    }
}
