using NUnit.Framework;
using Vendaloo.Models;

namespace Vendaloo.Tests
{
    [TestFixture]
    public class when_using_a_coin_model
    {
        [Test]
        public void it_should_set_the_value_correctly()
        {
            var value = 1m;
            var coin = new Coin(value);
            Assert.AreEqual(value, coin.Value);
        }

        [Test]
        public void it_should_format_the_value_as_currency()
        {
            var value = 1m;
            var coin = new Coin(value);
            Assert.AreEqual("£1.00", coin.AsCurrency);
        }
    }
}
