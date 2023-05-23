using Infrastructure;
using Infrastructure.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace RestArchitecture.Handlers.Items
{
    public class GetItemsHandler : IRequestHandler<GetItemsRequest, List<Item>>
    {
        private readonly CatalogContext _dbContext;

        public GetItemsHandler(CatalogContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Item>> Handle(GetItemsRequest request, CancellationToken cancellationToken)
        {
            var category = await _dbContext.Categories
                .Include(x => x.Items)
                .Where(x => x.Id == request.CategoryId)
                .FirstOrDefaultAsync(cancellationToken);

            if (category is null)
            {
                throw new Exception($"The category with id {request.CategoryId} does not exist");
            }

            return category.Items
                .Skip(request.PageSize * (request.PageNumber - 1))
                .Take(request.PageSize)
                .ToList();
        }
    }
}
