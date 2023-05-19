using MediatR;

namespace RestArchitecture.Handlers.Categories
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
