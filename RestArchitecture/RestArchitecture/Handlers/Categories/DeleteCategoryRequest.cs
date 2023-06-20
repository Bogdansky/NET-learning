using MediatR;

namespace RestArchitecture.Handlers.Categories
{
    public class DeleteCategoryRequest : IRequest<bool>
    {
        public int CategoryId { get; }
        public DeleteCategoryRequest(int categoryId)
        {
            CategoryId = categoryId;
        }
    }
}
