using ShopifyInventoryApi.DTOs;
using ShopifyInventoryApi.Models;

namespace ShopifyInventoryApi.Interfaces
{
    public interface IShipmentService
    {
        Task<Tuple<bool, ShipmentDto, string>> Create(CreateShipmentDto createShipmentDto);
        Task<List<ShipmentDto>> Get();
    }
}
