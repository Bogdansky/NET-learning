using Infrastructure;
using MediatR;

namespace RestArchitecture.Handlers.Catalogs
{
    public class DeleteCatalogHandler : IRequestHandler<DeleteCatalogRequest, bool>
    {
        private readonly CatalogContext _dbContext;

        public DeleteCatalogHandler(CatalogContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<bool> Handle(DeleteCatalogRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
