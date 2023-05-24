using Infrastructure;
using MediatR;

namespace RestArchitecture.Handlers.Items
{
    public class DeleteItemHandler : IRequestHandler<DeleteItemRequest, bool>
    {
        private readonly CatalogContext _dbContext;
        private readonly ILogger<DeleteItemHandler> _logger;

        public DeleteItemHandler(CatalogContext dbContext, ILogger<DeleteItemHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteItemRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var item = await _dbContext.Items.FindAsync(request.ItemId, cancellationToken);

                if (item is null)
                {
                    throw new Exception($"Item with id {request.ItemId} does not exist");
                }

                _dbContext.Items.Remove(item);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Something happened wrong");
                throw;
            }
        }
    }
}
