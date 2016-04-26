using System.Collections.Generic;
using FakeItEasy;
using NUnit.Framework;
using Vendaloo.Contracts;
using Vendaloo.Models;
using Vendaloo.Services;

namespace Vendaloo.Tests
{
    [TestFixture]
    public class when_using_a_product_service
    {
        [Test]
        public void it_should_list_products()
        {
            var products = new List<Product> { A.Fake<Product>(), A.Fake<Product>() };
            var productStore = A.Fake<IProductsStore>();
            A.CallTo(() => productStore.ListProducts()).Returns(products);
            var productService = new ManageProducts(productStore);

            var result = productService.ListAllProducts();
            Assert.That(result, Is.EqualTo(products));
        }
    }
}
