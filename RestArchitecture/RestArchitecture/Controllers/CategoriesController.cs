using Microsoft.AspNetCore.Mvc;

namespace RestArchitecture.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(ILogger<CategoriesController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "Categories_Introduction")]
        public IActionResult Index()
        {
            return Ok("Welcome to Categories controller");
        }
    }
}