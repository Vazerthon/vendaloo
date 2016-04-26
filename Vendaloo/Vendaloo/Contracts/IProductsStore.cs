using System.Collections.Generic;
using Vendaloo.Models;

namespace Vendaloo.Contracts
{
    // This would in reality be an orm wrapper or web service of some sort
    // I wouldn't ordinarily unit test my ORM so I'll leave test out for this
    public interface IProductsStore
    {
        IEnumerable<Product> ListProducts();
    }
}
