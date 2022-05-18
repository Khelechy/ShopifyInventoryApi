using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopifyInventoryApi.Data;
using ShopifyInventoryApi.DTOs;
using ShopifyInventoryApi.Interfaces;
using ShopifyInventoryApi.Models;

namespace ShopifyInventoryApi.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly InventoryDbContext _context;
        private readonly IMapper _mapper;
        private readonly IItemService _itemService;

        public InventoryService(InventoryDbContext context, IMapper mapper, IItemService itemService) => (_context, _mapper, _itemService) = (context, mapper, itemService);

        public async Task<Tuple<bool, InventoryDto, string>> Create(CreateInventoryDto createInventoryDto)
        {
            string error = string.Empty;
            var item = await _itemService.Get(createInventoryDto.ItemId);
            if(item is null)
            {
                error = "Item does not exist";
                return new Tuple<bool, InventoryDto, string>(false, null, error);
            }

            var inventoryRecordforItem = await GetByItemId(item.Id);
            if(inventoryRecordforItem is not null)
            {
                error = "An inventory record has already be added for this item";
                return new Tuple<bool, InventoryDto, string>(false, null, error);
            }

            var inventory = _mapper.Map<Inventory>(createInventoryDto);
            inventory.CreatedAt = DateTime.Now;
            inventory.UpdatedAt = DateTime.Now;

            await _context.Inventories.AddAsync(inventory);
            var created = await _context.SaveChangesAsync();

            var dto = _mapper.Map<InventoryDto>(inventory);

            return new Tuple<bool, InventoryDto, string>(true, dto, error);

        }

        public async Task<Inventory> Get(int inventoryId)
        {
            return await _context.Inventories.Include(x => x.Item).FirstOrDefaultAsync(p => p.Id == inventoryId);
        }

        public async Task<Inventory> GetByItemId(int itemId)
        {
            return await _context.Inventories.Include(x => x.Item).FirstOrDefaultAsync(p => p.ItemId == itemId);
        }

        public async Task<List<Inventory>> Get()
        {
            return await _context.Inventories.Where(x => !x.Item.IsSoftDelete).Include(x => x.Item ).ThenInclude(x => x.Product).ToListAsync();
        }

        public async Task<Tuple<bool, InventoryDto, string>> Edit(EditInventoryDto editInventoryDto)
        {
            string error = string.Empty;
            var inventory = await Get(editInventoryDto.InventoryId);
            if (inventory == null)
            {
                error = "Inventory not found";
                return new Tuple<bool, InventoryDto, string>(false, null, error);
            }

            inventory.Quantity = string.IsNullOrEmpty(editInventoryDto.Quantity.ToString()) ? inventory.Quantity : editInventoryDto.Quantity;
            inventory.UpdatedAt = DateTime.Now;

            _context.Update(inventory);
            var updated = await _context.SaveChangesAsync();

            var dto = _mapper.Map<InventoryDto>(inventory);

            return new Tuple<bool, InventoryDto, string>(updated > 0, dto, error);
        }

        public async Task<bool> AdjustInventory(int inventoryId, int shipmentQuantity)
        {
            var inv = await _context.Inventories.FirstOrDefaultAsync(x => x.Id == inventoryId);
            inv.Quantity = inv.Quantity - shipmentQuantity;
            inv.UpdatedAt = DateTime.Now;
            _context.Update(inv);
            var updated = await _context.SaveChangesAsync(); 
            return updated > 0;
        }
    }
}
