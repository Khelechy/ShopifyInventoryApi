using ShopifyInventoryApi.DTOs;
using ShopifyInventoryApi.Models;

namespace ShopifyInventoryApi.Interfaces
{
    public interface IProductService
    {
        Task<Tuple<bool, CreateProductDto>> Create(CreateProductDto createProductDto);
        Task<Tuple<bool, ProductDto, string>> Edit(EditProductDto editProductDto);
        Task<Tuple<bool, string>> Delete(int id);
        Task<Product> Get(int productId);
        Task<List<Product>> Get();
    }
}
