using MediatR;

namespace RestArchitecture.Handlers.Catalogs
{
    public class DeleteCatalogRequest : IRequest<bool>
    {
        public int CatalogId { get; }
        public DeleteCatalogRequest(int catalogId)
        {
            CatalogId = catalogId;
        }
    }
}
