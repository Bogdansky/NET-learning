using RestArchitecture.Models;
using MediatR;

namespace RestArchitecture.Handlers.Items
{
    public class AddItemRequest : IRequest<int>
    {
        public ItemDto Item { get; }
        public AddItemRequest(ItemDto item) 
        { 
            Item = item;
        }
    }
}
