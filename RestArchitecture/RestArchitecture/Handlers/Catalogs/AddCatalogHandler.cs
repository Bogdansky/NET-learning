using Infrastructure;
using Infrastructure.Models;
using MediatR;

namespace RestArchitecture.Handlers.Catalogs
{
    public class AddCatalogHandler : IRequestHandler<AddCatalogRequest, int>
    {
        private readonly CatalogContext _dbContext;

        public AddCatalogHandler(CatalogContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public Task<int> Handle(AddCatalogRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
