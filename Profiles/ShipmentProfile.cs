using AutoMapper;
using ShopifyInventoryApi.DTOs;
using ShopifyInventoryApi.Models;

namespace ShopifyInventoryApi.Profiles
{
    public class ShipmentProfile : Profile
    {
        public ShipmentProfile()
        {
            CreateMap<CreateShipmentDto, Shipment>().ReverseMap();
            CreateMap<Shipment, ShipmentDto>().ReverseMap();
        }   
    }
}
