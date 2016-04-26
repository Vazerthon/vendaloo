using System.Collections.Generic;
using FakeItEasy;
using NUnit.Framework;
using Vendaloo.Contracts;
using Vendaloo.Models;
using Vendaloo.Services;

namespace Vendaloo.Tests
{
    [TestFixture]
    public class when_using_a_vending_machine
    {
        [Test]
        public void it_should_be_able_to_produce_a_list_products()
        {
            var products = new List<Product> { A.Fake<Product>(), A.Fake<Product>() };
            var productsService = A.Fake<IManageProducts>();
            A.CallTo(() => productsService.ListAllProducts()).Returns(products);
            var vendingMachine = new VendingMachine(productsService);

            var result = vendingMachine.ListProducts();

            Assert.That(result, Is.EqualTo(products));
        }
    }
}
