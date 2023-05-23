using Infrastructure;
using Infrastructure.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace RestArchitecture.Handlers.Items
{
    public class AddItemHandler : IRequestHandler<AddItemRequest, int>
    {
        private readonly CatalogContext _dbContext;

        public AddItemHandler(CatalogContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle(AddItemRequest request, CancellationToken cancellationToken)
        {
            var categoryExists = await _dbContext.Categories
                .Where(x => x.Id == request.Item.CategoryId)
                .AnyAsync(cancellationToken);

            if (!categoryExists)
            {
                throw new Exception($"The category with id {request.Item.CategoryId} does not exist");
            }

            var item = new Item
            {
                Name = request.Item.Name,
                Description = request.Item.Description,
                CategoryId = request.Item.CategoryId,
                Price = request.Item.Price
            };

            _dbContext.Items.Add(item);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return item.Id;
        }
    }
}
