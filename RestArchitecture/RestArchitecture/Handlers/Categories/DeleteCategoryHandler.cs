using Infrastructure;
using MediatR;

namespace RestArchitecture.Handlers.Categories
{
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryRequest, bool>
    {
        private readonly CatalogContext _dbContext;

        public DeleteCategoryHandler(CatalogContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<bool> Handle(DeleteCategoryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
