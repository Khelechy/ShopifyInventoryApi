using ShopifyInventoryApi.DTOs;
using ShopifyInventoryApi.Models;

namespace ShopifyInventoryApi.Interfaces
{
    public interface IInventoryService
    {
        Task<Tuple<bool, InventoryDto, string>> Create(CreateInventoryDto createInventoryDto);
        Task<Inventory> Get(int inventoryId);
        Task<List<Inventory>> Get();

        Task<Tuple<bool, InventoryDto, string>> Edit(EditInventoryDto editInventoryDto);

        Task<bool> AdjustInventory(int inventoryId, int shipmentQuantity);
    }
}
