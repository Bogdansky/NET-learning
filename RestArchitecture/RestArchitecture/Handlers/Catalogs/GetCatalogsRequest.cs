using Infrastructure.Models;
using MediatR;

namespace RestArchitecture.Handlers.Catalogs
{
    public class GetCatalogsRequest : IRequest<List<Catalog>>
    {
        public int PageNumber { get; init; }  
        public int PageSize { get; init; }
    }
}
