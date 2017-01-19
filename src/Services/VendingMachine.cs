using System.Collections.Generic;
using System.Linq;
using Vendaloo.Contracts;
using Vendaloo.Models;

namespace Vendaloo.Services
{
    public class VendingMachine : IVendingMachine
    {
        readonly IManageProducts products;
        readonly IManageMoney money;

        public VendingMachine(IManageProducts products, IManageMoney money)
        {
            this.products = products;
            this.money = money;
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

            if (transaction.Product.Price > transaction.Funds)
            {
                return Error("Sorry. You have not inserted enough coins.");
            }

            if (transaction.Product.Stock <= 0)
            {
                return Error("Sorry. The item you have selected is out of stock.");
            }

            transaction.Product.ReduceStockBy(1);
            var change = transaction.Funds - transaction.Product.Price;
            return new TransactionResult
            {
                Success = true,
                Change = money.GetValueAsCoins(change)
            };
        }

        public IEnumerable<Coin> ListAllowedCoins()
        {
            return money.GetAllowedCoins();
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
