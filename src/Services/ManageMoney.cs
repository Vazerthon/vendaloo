using System.Collections.Generic;
using System.Linq;
using Vendaloo.Contracts;
using Vendaloo.Models;

namespace Vendaloo.Services
{
    public class ManageMoney : IManageMoney
    {
        public IEnumerable<Coin> GetAllowedCoins()
        {
            return new List<Coin>
            {
                new Coin(2.00M),
                new Coin(1.00M),
                new Coin(0.50M),
                new Coin(0.20M),
                new Coin(0.10M),
                new Coin(0.05M)
            };
        }

        public IEnumerable<Coin> GetValueAsCoins(decimal value)
        {
            var coins = GetAllowedCoins().ToArray();
            var split = new List<Coin>();
            foreach (var coin in coins)
            {
                if (value < coin.Value)
                {
                    continue;
                }

                split.Add(coin);
                value -= coin.Value;
            }

            return split;
        }
    }
}
