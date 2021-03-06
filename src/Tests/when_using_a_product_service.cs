﻿using System.Collections.Generic;
using NUnit.Framework;
using Vendaloo.Contracts;
using Vendaloo.Models;
using Vendaloo.Services;
using NSubstitute;

namespace Vendaloo.Tests
{
    [TestFixture]
    public class when_using_a_product_service
    {
        [Test]
        public void it_should_list_products()
        {
            var products = new List<Product> { new Product(0, "x", 1, 1),  new Product(0, "y", 2, 2) };
            var productStore = Substitute.For<IProductsStore>();
            productStore.ListProducts().Returns(products);

            var productService = new ManageProducts(productStore);

            var result = productService.ListAllProducts();
            Assert.That(result, Is.EqualTo(products));
        }
    }
}
