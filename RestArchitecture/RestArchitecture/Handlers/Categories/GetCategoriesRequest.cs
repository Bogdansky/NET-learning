using Infrastructure.Models;
using MediatR;
using RestArchitecture.Models;

namespace RestArchitecture.Handlers.Categories
{
    public class GetCategoriesRequest : IRequest<List<CategoryDto>> { }
}
