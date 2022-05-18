using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopifyInventoryApi.DTOs;
using ShopifyInventoryApi.Interfaces;
using ShopifyInventoryApi.Services;

namespace ShopifyInventoryApi.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService) => _productService = productService;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductDto createProductDto)
        {
            var (created, product) = await _productService.Create(createProductDto);
            if (!created)
            {
                return BadRequest("Error creating product");
            }

            return Created("api/products", product);

        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] EditProductDto editProductDto)
        {
            var (updated, item, error) = await _productService.Edit(editProductDto);
            if (!updated)
            {
                return BadRequest(error);
            }
            return Ok(item);

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.Get();
            return Ok(products);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _productService.Get(id);
            if(product == null)
            {
                return NotFound("Product not found");
            }
            return Ok(product);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var (deleted, error) = await _productService.Delete(id);
            if (deleted == false)
            {
                return BadRequest(error);
            }
            return Ok();

        }
    }
}
