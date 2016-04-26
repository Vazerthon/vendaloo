using System.Collections.Generic;
using Vendaloo.Contracts;
using Vendaloo.Models;

namespace Vendaloo.Services
{
    public class ManageProducts : IManageProducts
    {
        readonly IProductsStore products;

        public ManageProducts(IProductsStore products)
        {
            this.products = products;
        }

        public IEnumerable<Product> ListAllProducts()
        {
            return products.ListProducts();
        }
    }
}
