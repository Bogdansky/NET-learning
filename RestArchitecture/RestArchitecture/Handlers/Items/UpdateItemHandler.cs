using Infrastructure;
using Infrastructure.Models;
using MediatR;

namespace RestArchitecture.Handlers.Items
{
    public class UpdateItemHandler : IRequestHandler<UpdateItemRequest, bool>
    {
        private readonly CatalogContext _dbContext;

        public UpdateItemHandler(CatalogContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<bool> Handle(UpdateItemRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
