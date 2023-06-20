using Infrastructure.Models;
using MediatR;
using RestArchitecture.Models;

namespace RestArchitecture.Handlers.Items
{
    public class GetItemsRequest : IRequest<List<ItemDto>>
    {
        public int CategoryId { get; init; }
        public int PageNumber { get; init; }  
        public int PageSize { get; init; }
    }
}
