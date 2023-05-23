using Infrastructure.Models;
using MediatR;

namespace RestArchitecture.Handlers.Categories
{
    public class GetCategoriesRequest : IRequest<List<Category>> { }
}
