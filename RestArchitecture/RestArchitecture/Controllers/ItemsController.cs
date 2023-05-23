using MediatR;
using Microsoft.AspNetCore.Mvc;
using RestArchitecture.Constants;
using RestArchitecture.Handlers.Items;
using RestArchitecture.Models;
using Swashbuckle.AspNetCore.Annotations;

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

        [HttpGet("{categoryId}/items")]
        [SwaggerOperation(OperationId = "Items_GetItems")]
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

        [HttpPost]
        [SwaggerOperation(OperationId = "Items_AddItem")]
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

        [HttpPut]
        [SwaggerOperation(OperationId = "Items_UpdateItem")]
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

        [HttpDelete("{itemId}")]
        [SwaggerOperation(OperationId = "Items_DeleteItem")]
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
