using MediatR;
using Microsoft.AspNetCore.Mvc;
using RestArchitecture.Constants;
using RestArchitecture.Handlers.Categories;
using RestArchitecture.Handlers.Items;
using RestArchitecture.Models;

namespace RestArchitecture.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : Controller
    {
        private readonly ILogger<ItemsController> _logger;
        private readonly IMediator _mediator;

        public ItemsController(ILogger<ItemsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet(Name = "Items_GetItems")]
        [Route("{categoryId}/items")]
        public async Task<IActionResult> GetItems([FromRoute]int categoryId, [FromQuery]int pageNumber, [FromQuery]int pageSize)
        {
            try
            {
                var result = await _mediator.Send(new GetItemsRequest()
                {
                    CategoryId = categoryId,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                });

                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, ControllersConsts.ItemsExceptionTemplate, nameof(GetItems));
                return BadRequest(e.Message);
            }
        }

        [HttpPost(Name = "Items_AddItem")]
        public async Task<IActionResult> AddItem([FromBody] ItemDto item)
        {
            try
            {
                var result = await _mediator.Send(new AddItemRequest(item));

                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, ControllersConsts.ItemsExceptionTemplate, nameof(AddItem));
                return BadRequest();
            }
        }

        [HttpPut(Name = "Items_UpdateItem")]
        public async Task<IActionResult> UpdateItem([FromBody] ItemDto item)
        {
            try
            {
                var result = await _mediator.Send(new UpdateItemRequest(item));

                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, ControllersConsts.ItemsExceptionTemplate, nameof(UpdateItem));
                return BadRequest();
            }
        }

        [HttpDelete("{itemId}", Name = "Items_DeleteItem")]
        public async Task<IActionResult> DeleteItem(int itemId)
        {
            try
            {
                var result = await _mediator.Send(new DeleteItemRequest(itemId));

                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, ControllersConsts.ItemsExceptionTemplate, nameof(DeleteItem));
                return BadRequest();
            }
        }
    }
}
