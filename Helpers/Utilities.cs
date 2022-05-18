using ShopifyInventoryApi.DTOs;
using ShopifyInventoryApi.Models;

namespace ShopifyInventoryApi.Helpers
{
    public static class Utilities
    {
        public static string GenerateSku(Product product)
        {
            var name = product.Name.Replace(" ", "");
            return $"{name.Trim().Substring(0, 2)}/{product.Description.Trim().Substring(0, 2)}/{DateTime.Now.ToString().Substring(0, 4)}";
        }
    }
}
