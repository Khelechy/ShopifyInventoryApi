using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopifyInventoryApi.Data;
using ShopifyInventoryApi.DTOs;
using ShopifyInventoryApi.Interfaces;
using ShopifyInventoryApi.Models;

namespace ShopifyInventoryApi.Services
{
    public class ShipmentService : IShipmentService
    {
        private readonly InventoryDbContext _context;
        private readonly IMapper _mapper;
        private readonly IInventoryService _inventoryService;

        public ShipmentService(InventoryDbContext context, IMapper mapper, IInventoryService inventoryService) => (_context, _mapper, _inventoryService) = (context, mapper, inventoryService);
        public async Task<Tuple<bool, ShipmentDto, string>> Create(CreateShipmentDto createShipmentDto)
        {
            string error = string.Empty;
            var inventory = await _inventoryService.Get(createShipmentDto.InventoryId);
            if(inventory == null)
            {
                error = "Inventory record does not exist";
                return new Tuple<bool, ShipmentDto, string>(false, null, error);
            }

            //Has enough item quantity
            if(inventory.Quantity < createShipmentDto.Quantity)
            {
                error = "Inventory record does not have sufficient item quantity";
                return new Tuple<bool, ShipmentDto, string>(false, null, error);
            }  

            var shipment = _mapper.Map<Shipment>(createShipmentDto);
            shipment.CreatedAt = DateTime.Now;

            await _context.Shipments.AddAsync(shipment);
            var created = await _context.SaveChangesAsync();

            //Adjust Inventory
            if(created > 0)
            {
                await _inventoryService.AdjustInventory(inventory.Id, createShipmentDto.Quantity);
            }

            var dto = _mapper.Map<ShipmentDto>(shipment);

            return new Tuple<bool, ShipmentDto, string>(created > 0, dto, error);
        }

        public async Task<List<ShipmentDto>> Get()
        {
            var shipments = await _context.Shipments.Include(x => x.Inventory).ThenInclude(x => x.Item).ThenInclude(x => x.Product).ToListAsync();
            var dto = _mapper.Map<List<ShipmentDto>>(shipments);
            return dto;
        }
    }
}
