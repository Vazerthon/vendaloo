using System.Collections.Generic;
using System.Linq;
using Vendaloo.Contracts;
using Vendaloo.Models;

namespace Vendaloo.Services
{
    public class ManageMoney : IManageMoney
    {
        public IEnumerable<IMoney> GetAllowedDenominations()
        {
            return new List<IMoney>
            {
                new Coin(2.00M),
                new Coin(1.00M),
                new Coin(0.50M),
                new Coin(0.20M),
                new Coin(0.10M),
                new Coin(0.05M)
            };
        }

        public IEnumerable<IMoney> GetValueAsMoney(decimal value)
        {
            var coins = GetAllowedDenominations().ToArray();
            var split = new List<IMoney>();
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
