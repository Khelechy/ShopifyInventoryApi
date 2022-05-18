using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopifyInventoryApi.DTOs;
using ShopifyInventoryApi.Interfaces;

namespace ShopifyInventoryApi.Controllers
{
    [Route("api/inventories")]
    [ApiController]
    public class InventoriesController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;
        public InventoriesController(IInventoryService inventoryService) => _inventoryService = inventoryService;


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateInventoryDto createInventoryDto)
        {
            var (created, inventory, error) = await _inventoryService.Create(createInventoryDto);
            if (!created)
            {
                return BadRequest(error);
            }

            return Created("api/inventories", inventory);

        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] EditInventoryDto editInventoryDto)
        {
            var (updated, inventory, error) = await _inventoryService.Edit(editInventoryDto);
            if (!updated)
            {
                return BadRequest(error);
            }
            return Ok(inventory);

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var inventories = await _inventoryService.Get();
            return Ok(inventories);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var inventory = await _inventoryService.Get(id);
            if (inventory == null)
            {
                return NotFound("Inventory not found");
            }
            return Ok(inventory);

        }
    }


}
