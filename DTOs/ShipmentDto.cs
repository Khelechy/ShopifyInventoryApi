using ShopifyInventoryApi.Models;
using System.ComponentModel.DataAnnotations;

namespace ShopifyInventoryApi.DTOs
{
    public class ShipmentDto
    {
        public int Id { get; set; }
        public int InventoryId { get; set; }
        public Inventory Inventory { get; set; }
        public int Quantity { get; set; }
        public string Location { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateShipmentDto
    {
        [Required]
        public int InventoryId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string Location { get; set; }
    }
}
