using AutoMapper;
using ShopifyInventoryApi.DTOs;
using ShopifyInventoryApi.Models;

namespace ShopifyInventoryApi.Profiles
{
    public class InventoryProfile : Profile
    {
        public InventoryProfile()
        {
            CreateMap<CreateInventoryDto, Inventory>();
            CreateMap<Inventory, InventoryDto>();
        }
    }
}
