using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopifyInventoryApi.Data;
using ShopifyInventoryApi.DTOs;
using ShopifyInventoryApi.Helpers;
using ShopifyInventoryApi.Interfaces;
using ShopifyInventoryApi.Models;

namespace ShopifyInventoryApi.Services
{
    public class ItemService : IItemService
    {
        private readonly InventoryDbContext _context;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public ItemService(InventoryDbContext context, IMapper mapper, IProductService productService) => (_context, _mapper, _productService) = (context, mapper, productService);
        public async Task<Tuple<bool, CreateItemDto, string>> Create(CreateItemDto createItemDto)
        {
            string error = string.Empty;

            var product = await _productService.Get(createItemDto.ProductId);
            if(product == null)
            {
                error = "Product is not found";
                return new Tuple<bool, CreateItemDto, string>(false, null, error);
            }

            var inventoryItem = await GetItemByProductId(product.Id); 
            if(inventoryItem != null)
            {
                error = "This product already exist as an inventory item";
                return new Tuple<bool, CreateItemDto, string>(false, null, error);
            }

            var item = _mapper.Map<Item>(createItemDto);
            item.CreatedAt = DateTime.Now;
            item.UpdatedAt = DateTime.Now;
            item.SKU = Utilities.GenerateSku(product);
            
            await _context.Items.AddAsync(item);
            var created = await _context.SaveChangesAsync();

            return new Tuple<bool, CreateItemDto, string>(created > 0, createItemDto, error);
        }

        public async Task<Tuple<bool, ItemDto, string>> Edit(EditItemDto editItemDto)
        {
            string error = string.Empty;
            var item = await Get(editItemDto.ItemId);
            if (item == null)
            {
                error = "Item not found";
                return new Tuple<bool, ItemDto, string>(false, null, error);
            }

            item.SKU = string.IsNullOrEmpty(editItemDto.Sku) ? item.SKU : editItemDto.Sku;
            item.Cost = editItemDto.Cost == null ? item.Cost : (decimal)editItemDto.Cost;
            item.UpdatedAt = DateTime.Now;

            _context.Update(item);
            var updated = await _context.SaveChangesAsync();

            var dto = _mapper.Map<ItemDto>(item);

            return new Tuple<bool, ItemDto, string>(updated > 0, dto, error);
        }

        public async Task<List<ItemDto>> Get()
        {
            var items = await _context.Items.Include(x => x.Product).Where(i => i.IsSoftDelete != true).ToListAsync();
            var dto = _mapper.Map<List<ItemDto>>(items);
            return dto;
        }

        public async Task<Item> Get(int itemId)
        {
            return await _context.Items.Include(x => x.Product).FirstOrDefaultAsync(i => i.Id == itemId && i.IsSoftDelete != true);
        }

        public async Task<Item> GetItemByProductId(int productId)
        {
            return await _context.Items.Include(x => x.Product).FirstOrDefaultAsync(i => i.ProductId == productId);
        }

        public async Task<Tuple<bool, string>> Delete(int itemId)
        {
            string error = string.Empty;
            var item = await Get(itemId);
            if (item == null)
            {
                error = "Item not found";
                return new Tuple<bool, string>(false, error);
            }

            item.IsSoftDelete = true;

            _context.Update(item);

            var updated = await _context.SaveChangesAsync();

            return new Tuple<bool, string>(updated > 0, error);
        }

        public async Task<bool> UnDelete(int itemId)
        {
            var item = await Get(itemId);
            if(item == null)
            {
                throw new ArgumentNullException("Item not found");
            }

            item.IsSoftDelete = false;
            item.Comment = String.Empty;

            _context.Update(item);

            var updated = await _context.SaveChangesAsync();

            return updated > 0;
        }
    }
}
