using Infrastructure;
using Infrastructure.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RestArchitecture.Models;

namespace RestArchitecture.Handlers.Categories
{
    public class GetCategoriesHandler : IRequestHandler<GetCategoriesRequest, List<CategoryDto>>
    {
        private readonly CatalogContext _dbContext;

        public GetCategoriesHandler(CatalogContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<CategoryDto>> Handle(GetCategoriesRequest request, CancellationToken cancellationToken)
        {
            return await _dbContext.Categories
                .Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .ToListAsync(cancellationToken);
        }
    }
}
