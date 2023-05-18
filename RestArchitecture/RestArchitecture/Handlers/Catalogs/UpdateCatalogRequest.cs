using RestArchitecture.Models;
using MediatR;

namespace RestArchitecture.Handlers.Catalogs
{
    public class UpdateCatalogRequest : IRequest<bool>
    {
        public CatalogDto Catalog { get; }
        public UpdateCatalogRequest(CatalogDto catalog)
        {
            Catalog = catalog;
        }
    }
}
