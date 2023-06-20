using Infrastructure;
using Infrastructure.Models;
using MediatR;

namespace RestArchitecture.Handlers.Categories
{
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryRequest, bool>
    {
        private readonly CatalogContext _dbContext;

        public UpdateCategoryHandler(CatalogContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(UpdateCategoryRequest request, CancellationToken cancellationToken)
        {
            if (request.Category is null)
            {
                throw new ArgumentNullException(nameof(request.Category));
            }

            var category = await _dbContext.Categories.FindAsync(request.Category.Id, cancellationToken);

            if (category is null)
            {
                throw new Exception($"The category with id {request.Category.Id} does not exist");
            }

            category.Name = request.Category.Name;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
