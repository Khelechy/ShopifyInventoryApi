using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopifyInventoryApi.Data;
using ShopifyInventoryApi.DTOs;
using ShopifyInventoryApi.Interfaces;
using ShopifyInventoryApi.Models;

namespace ShopifyInventoryApi.Services
{
    public class ProductService : IProductService
    {
        private readonly InventoryDbContext _context;
        private readonly IMapper _mapper;

        public ProductService(InventoryDbContext context, IMapper mapper) => (_context, _mapper) = (context, mapper);

        public async Task<Tuple<bool, CreateProductDto>> Create(CreateProductDto createProductDto)
        {
            var product = _mapper.Map<Product>(createProductDto);
            product.CreatedAt = DateTime.Now; 
            product.UpdatedAt = DateTime.Now;  
            await _context.Products.AddAsync(product);
            var created = await _context.SaveChangesAsync();


            return new Tuple<bool, CreateProductDto>(created > 0, createProductDto);
        }

        public async Task<Tuple<bool, string>> Delete(int id)
        {
            string error = string.Empty;
            var product = await Get(id);
            if (product == null)
            {
                error = "Product not found";
                return new Tuple<bool, string>(false, error);
            }

            _context.Products.Remove(product);
            var deleted = await _context.SaveChangesAsync();

            return new Tuple<bool, string>(deleted > 0, "");
        }

        public async Task<Tuple<bool, ProductDto, string>> Edit(EditProductDto editProductDto)
        {
            string error = string.Empty;
            var product = await Get(editProductDto.ProductId);
            if (product == null)
            {
                error = "Product not found";
                return new Tuple<bool, ProductDto, string>(false, null, error);
            }


            product.Name = string.IsNullOrEmpty(editProductDto.Name) ? product.Name : editProductDto.Name;
            product.Description = string.IsNullOrEmpty(editProductDto.Description) ? product.Description : editProductDto.Description;
            product.UpdatedAt = DateTime.Now;

            _context.Update(product);
            var updated = await _context.SaveChangesAsync();

            var dto = _mapper.Map<ProductDto>(product);

            return new Tuple<bool, ProductDto, string>(updated > 0, dto, error);
        }

        public async Task<Product> Get(int productId)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
        }

        public async Task<List<Product>> Get()
        {
            return await _context.Products.ToListAsync();
        }
    }
}
