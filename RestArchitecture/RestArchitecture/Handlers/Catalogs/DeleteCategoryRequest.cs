using MediatR;

namespace RestArchitecture.Handlers.Catalogs
{
    public class DeleteCategoryRequest : IRequest<bool>
    {
        public int CatalogId { get; }
        public DeleteCategoryRequest(int catalogId)
        {
            CatalogId = catalogId;
        }
    }
}
