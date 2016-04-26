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
                new Product {Id = 0, Name = "Vindaloo", Price = 9.95m, Stock = 5 },
                new Product {Id = 1, Name = "Tikka", Price = 10.50m, Stock = 0 },
                new Product {Id = 2, Name = "Bhuna", Price = 10m, Stock = 12 },
                new Product {Id = 3, Name = "Korma", Price = 6.55m, Stock = 2 },
                new Product {Id = 4, Name = "Jalfrezi", Price = 12.99m, Stock = 20 },
            };
        }

        public IEnumerable<Product> ListProducts()
        {
            return products;
        }
    }
}
