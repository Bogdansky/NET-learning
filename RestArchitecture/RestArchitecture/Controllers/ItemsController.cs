using Microsoft.AspNetCore.Mvc;

namespace RestArchitecture.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ILogger<ItemsController> _logger;

        public ItemsController(ILogger<ItemsController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "Items_Introduction")]
        public IActionResult Index()
        {
            return Ok("Welcome to Items controller");
        }
    }
}
