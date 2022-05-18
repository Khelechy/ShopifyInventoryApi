using ShopifyInventoryApi.DTOs;
using ShopifyInventoryApi.Models;

namespace ShopifyInventoryApi.Interfaces
{
    public interface IItemService
    {
        Task<List<ItemDto>> Get();
        Task<Item> Get(int itemId);
        Task<Tuple<bool, CreateItemDto, string>> Create(CreateItemDto createItemDto);
        Task<Tuple<bool, ItemDto, string>> Edit(EditItemDto editItemDto);
        Task<Tuple<bool, string>> Delete(int itemId);
        Task<bool> UnDelete(int itemId);
    }
}
