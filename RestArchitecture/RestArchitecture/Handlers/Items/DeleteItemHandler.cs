using Infrastructure;
using MediatR;

namespace RestArchitecture.Handlers.Items
{
    public class DeleteItemHandler : IRequestHandler<DeleteItemRequest, bool>
    {
        private readonly CatalogContext _dbContext;

        public DeleteItemHandler(CatalogContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<bool> Handle(DeleteItemRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
