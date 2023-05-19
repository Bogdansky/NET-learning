using Infrastructure;
using Infrastructure.Models;
using MediatR;

namespace RestArchitecture.Handlers.Catalogs
{
    public class AddCategoryHandler : IRequestHandler<AddCategoryRequest, int>
    {
        private readonly CatalogContext _dbContext;

        public AddCategoryHandler(CatalogContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public Task<int> Handle(AddCategoryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
