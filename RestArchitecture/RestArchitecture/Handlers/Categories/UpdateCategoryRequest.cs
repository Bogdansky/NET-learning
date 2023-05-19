using RestArchitecture.Models;
using MediatR;

namespace RestArchitecture.Handlers.Categories
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
