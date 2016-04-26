using System.Collections.Generic;
using Vendaloo.Models;

namespace Vendaloo.Contracts
{
    public interface IVendingMachine
    {
        IEnumerable<Product> ListProducts();

        TransactionResult PurchaseProduct(Transaction transaction);
    }
}
