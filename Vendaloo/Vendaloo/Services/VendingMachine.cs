using System.Collections.Generic;
using System.Linq;
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

        public TransactionResult PurchaseProduct(Transaction transaction)
        {
            if (products.ListAllProducts().SingleOrDefault(p => p.Id.Equals(transaction.Product.Id)) == null)
            {
                return Error("Sorry. The item you have selected is unknown.");
            }

            if (transaction.Product.Stock <= 0)
            {
                return Error("Sorry. The item you have selected is out of stock.");
            }

            transaction.Product.Stock--;
            return new TransactionResult
            {
                Success = true
            };
        }

        TransactionResult Error(string message)
        {
            return new TransactionResult
            {
                Success = false,
                Error = message
            };
        }
    }
}
