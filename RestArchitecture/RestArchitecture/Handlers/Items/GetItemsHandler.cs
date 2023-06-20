using Infrastructure;
using Infrastructure.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RestArchitecture.Models;

namespace RestArchitecture.Handlers.Items
{
    public class GetItemsHandler : IRequestHandler<GetItemsRequest, List<ItemDto>>
    {
        private readonly CatalogContext _dbContext;

        public GetItemsHandler(CatalogContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ItemDto>> Handle(GetItemsRequest request, CancellationToken cancellationToken)
        {
            var items = await _dbContext.Items
                .Where(x => x.CategoryId == request.CategoryId)
                .Skip(request.PageSize * (request.PageNumber - 1))
                .Take(request.PageSize)
                .Select(x => new ItemDto
                {
                    Id= x.Id,
                    Name = x.Name,
                    Description= x.Description, 
                    Price = x.Price,
                    CategoryId = x.CategoryId,
                })
                .ToListAsync(cancellationToken);

            if (items is null || items.Count == 0)
            {
                throw new Exception($"No items with category id {request.CategoryId}");
            }

            return items;
        }
    }
}
