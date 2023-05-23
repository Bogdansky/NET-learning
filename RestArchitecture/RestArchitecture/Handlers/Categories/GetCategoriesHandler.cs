using Infrastructure;
using Infrastructure.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace RestArchitecture.Handlers.Categories
{
    public class GetCategoriesHandler : IRequestHandler<GetCategoriesRequest, List<Category>>
    {
        private readonly CatalogContext _dbContext;

        public GetCategoriesHandler(CatalogContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Category>> Handle(GetCategoriesRequest request, CancellationToken cancellationToken)
        {
            return await _dbContext.Categories.ToListAsync(cancellationToken);
        }
    }
}
