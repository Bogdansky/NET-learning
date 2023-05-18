using Infrastructure;
using Infrastructure.Models;
using MediatR;

namespace RestArchitecture.Handlers.Catalogs
{
    public class GetCatalogsHandler : IRequestHandler<GetCatalogsRequest, List<Catalog>>
    {
        private readonly CatalogContext _dbContext;

        public GetCatalogsHandler(CatalogContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Catalog>> Handle(GetCatalogsRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
