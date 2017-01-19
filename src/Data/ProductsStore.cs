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
                new Product(0, "Vindaloo", 9.95m, 5),
                new Product(1, "Tikka", 10.50m, 0),
                new Product(2, "Bhuna", 10m, 12),
                new Product(3, "Korma", 6.55m, 2),
                new Product(4, "Jalfrezi", 12.95m, 20),
            };
        }

        public IEnumerable<Product> ListProducts()
        {
            return products;
        }
    }
}
