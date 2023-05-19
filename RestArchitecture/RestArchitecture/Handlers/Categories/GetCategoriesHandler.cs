using Infrastructure;
using Infrastructure.Models;
using MediatR;

namespace RestArchitecture.Handlers.Categories
{
    public class GetCategoriesHandler : IRequestHandler<GetCategoriesRequest, List<Category>>
    {
        private readonly CatalogContext _dbContext;

        public GetCategoriesHandler(CatalogContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Category>> Handle(GetCategoriesRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
