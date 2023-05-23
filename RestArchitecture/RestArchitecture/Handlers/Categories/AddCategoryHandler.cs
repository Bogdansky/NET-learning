using Infrastructure;
using Infrastructure.Models;
using MediatR;

namespace RestArchitecture.Handlers.Categories
{
    public class AddCategoryHandler : IRequestHandler<AddCategoryRequest, int>
    {
        private readonly CatalogContext _dbContext;

        public AddCategoryHandler(CatalogContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle(AddCategoryRequest request, CancellationToken cancellationToken)
        {
            if (request.Category is null)
            {
                throw new ArgumentNullException(nameof(request.Category));
            }

            var category = new Category
            {
                Name = request.Category.Name
            };

            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return category.Id;
        }
    }
}
