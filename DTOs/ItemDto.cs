using ShopifyInventoryApi.Models;
using System.ComponentModel.DataAnnotations;

namespace ShopifyInventoryApi.DTOs
{
    public class ItemDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string? SKU { get; set; }
        public decimal Cost { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsSoftDelete { get; set; }
        public string? Comment { get; set; }
    }

    public class CreateItemDto
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public decimal Cost { get; set; }
    }

    public class EditItemDto
    {
        [Required]
        public int ItemId { get; set; }
        public string? Sku { get; set; }
        public decimal? Cost { get; set; }
    }


    public class DeleteItemDto
    {
        [Required]
        public int ItemId { get; set; }
        [Required]
        public string Comment { get; set; }
    }
}
