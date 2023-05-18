using Infrastructure.Models;
using MediatR;

namespace RestArchitecture.Handlers.Items
{
    public class GetItemsRequest : IRequest<List<Item>>
    {
        public int CategoryId { get; init; }
        public int PageNumber { get; init; }  
        public int PageSize { get; init; }
    }
}
