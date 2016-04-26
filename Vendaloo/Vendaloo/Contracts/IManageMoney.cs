using System.Collections.Generic;
using Vendaloo.Models;

namespace Vendaloo.Contracts
{
    public interface IManageMoney
    {
        IEnumerable<Coin> GetAllowedCoins();

        IEnumerable<Coin> GetValueAsCoins(decimal value);
    }
}
