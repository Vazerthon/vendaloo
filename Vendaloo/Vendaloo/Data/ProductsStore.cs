using System.Collections.Generic;
using Vendaloo.Contracts;
using Vendaloo.Models;

namespace Vendaloo.Data
{
    public class ProductsStore : IProductsStore
    {
        readonly IList<Product> products;

        public ProductsStore()
        {
            products = new List<Product>
            {
                new Product { Name = "Vindaloo", Price = 9.95m, Stock = 5 },
                new Product { Name = "Tikka Masala", Price = 10.50m, Stock = 0 },
                new Product { Name = "Bhuna", Price = 10m, Stock = 12 },
                new Product { Name = "Korma", Price = 6.55m, Stock = 2 },
                new Product { Name = "Jalfrezi", Price = 12.99m, Stock = 20 },
            };
        }

        public IEnumerable<Product> ListProducts()
        {
            return products;
        }
    }
}
