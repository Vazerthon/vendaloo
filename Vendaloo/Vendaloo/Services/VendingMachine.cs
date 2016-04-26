using System.Collections.Generic;
using Vendaloo.Contracts;
using Vendaloo.Models;

namespace Vendaloo.Services
{
    public class VendingMachine : IVendingMachine
    {
        readonly IManageProducts products;

        public VendingMachine(IManageProducts products)
        {
            this.products = products;
        }

        public IEnumerable<Product> ListProducts()
        {
            return products.ListAllProducts();
        }
    }
}
