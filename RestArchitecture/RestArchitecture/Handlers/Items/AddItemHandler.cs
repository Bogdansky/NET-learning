using Infrastructure;
using Infrastructure.Models;
using MediatR;

namespace RestArchitecture.Handlers.Items
{
    public class AddItemHandler : IRequestHandler<AddItemRequest, int>
    {
        private readonly CatalogContext _dbContext;

        public AddItemHandler(CatalogContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<int> Handle(AddItemRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
