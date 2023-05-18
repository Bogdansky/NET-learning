using RestArchitecture.Models;
using MediatR;

namespace RestArchitecture.Handlers.Catalogs
{
    public class AddCatalogRequest : IRequest<int>
    {
        public CatalogDto Catalog { get; }

        public AddCatalogRequest(CatalogDto catalog) 
        { 
            Catalog = catalog;
        }
    }
}
