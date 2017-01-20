using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Vendaloo.Models;
using Vendaloo.Services;

namespace Vendaloo.Tests
{
    [TestFixture]
    public class when_using_a_money_service
    {
        [Test]
        public void it_should_correctly_return_a_value_split_into_coins()
        {
            const decimal input = 3.75m;
            var moneyManager = new ManageMoney();
            var result = moneyManager.GetValueAsMoney(input).ToList();

            Assert.That(result.Sum(c => c.Value), Is.EqualTo(input));
            var expected = new List<IMoney>
            {
                new Coin(2.00M),
                new Coin(1.00M),
                new Coin(0.50M),
                new Coin(0.20M),
                new Coin(0.05M)
            };

            foreach (var coin in expected)
            {
                Assert.That(result.Any(c => c.Value.Equals(coin.Value)), Is.True);
            }
        }

        // TODO: tests when handling notes
    }
}
