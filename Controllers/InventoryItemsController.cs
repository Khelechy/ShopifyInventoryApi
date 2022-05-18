using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopifyInventoryApi.DTOs;
using ShopifyInventoryApi.Interfaces;

namespace ShopifyInventoryApi.Controllers
{
    [Route("api/inventoryitems")]
    [ApiController]
    public class InventoryItemsController : ControllerBase
    {
        private readonly IItemService _itemService;
        public InventoryItemsController(IItemService itemService) => _itemService = itemService;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateItemDto createItemDto)
        {
            var (created, product, error) = await _itemService.Create(createItemDto);
            if (!created)
            {
                return BadRequest(error);
            }

            return Created("api/inventoryItems", product);

        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] EditItemDto editItemDto)
        {
            var (updated, item, error) = await _itemService.Edit(editItemDto);
            if (!updated)
            {
                return BadRequest(error);
            }
            return Ok(item);

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _itemService.Get();
            return Ok(items);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _itemService.Get(id);
            if (item == null)
            {
                return NotFound("Item not found");
            }
            return Ok(item);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var (deleted, error) = await _itemService.Delete(id);
            if (!deleted)
            {
                return BadRequest(error);
            }
            return Ok();

        }

        [HttpPut("undelete/{id}")]
        public async Task<IActionResult> UnDelete(int id)
        {
            var deleted = await _itemService.UnDelete(id);
            if (!deleted)
            {
                return NotFound("Error undeleting item");
            }
            return Ok();

        }
    }
}
