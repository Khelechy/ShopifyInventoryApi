using AutoMapper;
using ShopifyInventoryApi.DTOs;
using ShopifyInventoryApi.Models;

namespace ShopifyInventoryApi.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<CreateProductDto, Product>().ReverseMap();
            CreateMap<ProductDto, Product>().ReverseMap();
        }
    }
}
