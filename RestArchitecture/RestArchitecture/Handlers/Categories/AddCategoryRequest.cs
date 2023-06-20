using RestArchitecture.Models;
using MediatR;

namespace RestArchitecture.Handlers.Categories
{
    public class AddCategoryRequest : IRequest<int>
    {
        public CategoryDto Category { get; }

        public AddCategoryRequest(CategoryDto category) 
        {
            Category = category;
        }
    }
}
