using RestArchitecture.Models;
using MediatR;

namespace RestArchitecture.Handlers.Categories
{
    public class AddCategoryRequest : IRequest<int>
    {
        public CategoryDto Catalog { get; }

        public AddCategoryRequest(CategoryDto catalog) 
        { 
            Catalog = catalog;
        }
    }
}
