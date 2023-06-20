using RestArchitecture.Models;
using MediatR;
using Infrastructure.Models;

namespace RestArchitecture.Handlers.Categories
{
    public class UpdateCategoryRequest : IRequest<bool>
    {
        public CategoryDto Category { get; }
        public UpdateCategoryRequest(CategoryDto category)
        {
            Category = category;
        }
    }
}
