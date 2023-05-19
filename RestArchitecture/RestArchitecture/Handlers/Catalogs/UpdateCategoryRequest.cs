using RestArchitecture.Models;
using MediatR;

namespace RestArchitecture.Handlers.Catalogs
{
    public class UpdateCategoryRequest : IRequest<bool>
    {
        public CategoryDto Catalog { get; }
        public UpdateCategoryRequest(CategoryDto catalog)
        {
            Catalog = catalog;
        }
    }
}
