using Infrastructure;
using Infrastructure.Models;
using MediatR;

namespace RestArchitecture.Handlers.Catalogs
{
    public class UpdateCatalogHandler : IRequestHandler<UpdateCatalogRequest, bool>
    {
        private readonly CatalogContext _dbContext;

        public UpdateCatalogHandler(CatalogContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<bool> Handle(UpdateCatalogRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
