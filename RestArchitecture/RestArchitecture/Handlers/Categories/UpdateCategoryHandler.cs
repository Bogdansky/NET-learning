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

        public Task<bool> Handle(UpdateCategoryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
