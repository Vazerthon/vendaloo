using System.Collections;
using System.Collections.Generic;
using Vendaloo.Models;

namespace Vendaloo.Contracts
{
    public interface IManageProducts
    {
        IEnumerable<Product> ListAllProducts();
    }
}
