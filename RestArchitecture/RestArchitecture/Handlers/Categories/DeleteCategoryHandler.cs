using Infrastructure;
using MediatR;

namespace RestArchitecture.Handlers.Categories
{
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryRequest, bool>
    {
        private readonly CatalogContext _dbContext;
        private readonly ILogger<DeleteCategoryHandler> _logger;

        public DeleteCategoryHandler(CatalogContext dbContext, ILogger<DeleteCategoryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteCategoryRequest request, CancellationToken cancellationToken)
        {
            var category = await _dbContext.Categories.FindAsync(request.CategoryId, cancellationToken);

            if (category is null)
            {
                throw new Exception($"The category with id {request.CategoryId} does not exist");
            }

            try
            {
                _dbContext.Categories.Remove(category);

                await _dbContext.SaveChangesAsync(cancellationToken);

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Something happened wrong");
                throw;
            }
        }
    }
}
