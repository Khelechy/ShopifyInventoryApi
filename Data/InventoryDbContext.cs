using Microsoft.EntityFrameworkCore;
using ShopifyInventoryApi.Models;

namespace ShopifyInventoryApi.Data
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Shipment> Shipments { get; set; }

    }
}
