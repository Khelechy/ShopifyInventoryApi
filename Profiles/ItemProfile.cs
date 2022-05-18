using AutoMapper;
using ShopifyInventoryApi.DTOs;
using ShopifyInventoryApi.Models;

namespace ShopifyInventoryApi.Profiles
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<CreateItemDto, Item>();
            CreateMap<Item, ItemDto>();
        }
    }
}
