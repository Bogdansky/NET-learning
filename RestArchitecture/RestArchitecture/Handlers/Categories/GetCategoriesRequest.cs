using Infrastructure.Models;
using MediatR;

namespace RestArchitecture.Handlers.Categories
{
    public class GetCategoriesRequest : IRequest<List<Category>>
    {
        public int PageNumber { get; init; }  
        public int PageSize { get; init; }
    }
}
