using Infrastructure;
using Infrastructure.Models;
using MediatR;

namespace RestArchitecture.Handlers.Items
{
    public class GetItemsHandler : IRequestHandler<GetItemsRequest, List<Item>>
    {
        private readonly CatalogContext _dbContext;

        public GetItemsHandler(CatalogContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Item>> Handle(GetItemsRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
