using ShopifyInventoryApi.Models;

namespace ShopifyInventoryApi.DTOs
{
    public class InventoryDto
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public int Quantity { get; set; }
    }

    public class CreateInventoryDto
    {
        public int ItemId { get; set; }
        public int Quantity { get; set; }
    }

    public class EditInventoryDto
    {
        public int InventoryId { get; set; }
        public int Quantity { get; set; }
    }
}
