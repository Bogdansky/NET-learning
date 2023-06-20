using RestArchitecture.Models;
using MediatR;

namespace RestArchitecture.Handlers.Items
{
    public class UpdateItemRequest : IRequest<bool>
    {
        public ItemDto Item { get; }
        public UpdateItemRequest(ItemDto item)
        {
            Item = item;
        }
    }
}
