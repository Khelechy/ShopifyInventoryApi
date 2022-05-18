using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopifyInventoryApi.DTOs;
using ShopifyInventoryApi.Interfaces;

namespace ShopifyInventoryApi.Controllers
{
    [Route("api/shipments")]
    [ApiController]
    public class ShipmentsController : ControllerBase
    {
        private readonly IShipmentService _shipmentService;
        public ShipmentsController(IShipmentService shipmentService) => _shipmentService = shipmentService;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateShipmentDto createShipmentDto)
        {
            var (created, shipment, error) = await _shipmentService.Create(createShipmentDto);
            if (!created)
            {
                return BadRequest(error);
            }

            return Created("api/shipments", shipment);

        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var shipments = await _shipmentService.Get();
           return Ok(shipments);    

        }
    }
}
