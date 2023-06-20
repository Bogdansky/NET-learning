using RestArchitecture.Controllers;

namespace RestArchitecture.Constants
{
    public class ControllersConsts
    {
        public const string CategoriesExceptionTemplate = $"Error raised in {{0}} action of {nameof(CategoriesController)}";
        public const string ItemsExceptionTemplate = $"Error raised in {{0}} action of {nameof(CategoriesController)}";
    }
}
