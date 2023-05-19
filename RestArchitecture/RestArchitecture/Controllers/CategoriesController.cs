using Infrastructure.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RestArchitecture.Constants;
using RestArchitecture.Handlers.Catalogs;
using RestArchitecture.Models;

namespace RestArchitecture.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ILogger<CategoriesController> _logger;
        private readonly IMediator _mediator;

        public CategoriesController(ILogger<CategoriesController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet(Name = "Categories_GetCategories")]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var result = await _mediator.Send(new GetCategoriesRequest());

                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, ControllersConsts.CategoriesExceptionTemplate, nameof(GetCategories));
                return BadRequest();
            }
        }

        [HttpPost(Name = "Categories_AddCategory")]
        public async Task<IActionResult> AddCategory([FromBody]CategoryDto category)
        {
            try
            {
                var result = await _mediator.Send(new AddCategoryRequest(category));

                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, ControllersConsts.CategoriesExceptionTemplate, nameof(AddCategory));
                return BadRequest();
            }
        }

        [HttpPut(Name = "Categories_UpdateCategory")]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryDto category)
        {
            try
            {
                var result = await _mediator.Send(new UpdateCategoryRequest(category));

                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, ControllersConsts.CategoriesExceptionTemplate, nameof(UpdateCategory));
                return BadRequest();
            }
        }

        [HttpDelete(Name = "Categories_DeleteCategory")]
        [Route("{categoryId}")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            try
            {
                var result = await _mediator.Send(new DeleteCategoryRequest(categoryId));

                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, ControllersConsts.CategoriesExceptionTemplate, nameof(DeleteCategory));
                return BadRequest();
            }
        }
    }
}