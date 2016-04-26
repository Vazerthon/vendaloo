﻿using System.Collections.Generic;
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
            var products = new List<Product> {A.Fake<Product>(), A.Fake<Product>()};
            var productsService = A.Fake<IManageProducts>();
            var moneyService = A.Fake<IManageMoney>();
            A.CallTo(() => productsService.ListAllProducts()).Returns(products);
            var vendingMachine = new VendingMachine(productsService, moneyService);

            var result = vendingMachine.ListProducts();

            Assert.That(result, Is.EqualTo(products));
        }

        [Test]
        public void it_should_allow_a_product_to_be_purchased()
        {
            var product = new Product {Id = 2, Name = "fake", Stock = 1, Price = 5};
            var products = new List<Product> {product};
            var productsService = A.Fake<IManageProducts>();
            var moneyService = A.Fake<IManageMoney>();
            A.CallTo(() => productsService.ListAllProducts()).Returns(products);
            var vendingMachine = new VendingMachine(productsService, moneyService);

            var transaction = new Transaction {Product = product, Funds = 10m};
            var result = vendingMachine.PurchaseProduct(transaction);

            Assert.That(result.Success, Is.True);
        }

        [Test]
        public void it_should_reduce_the_stock_count_after_a_transaction()
        {
            var product = new Product {Id = 2, Name = "fake", Stock = 1, Price = 5};
            var products = new List<Product> {product};
            var productsService = A.Fake<IManageProducts>();
            var moneyService = A.Fake<IManageMoney>();
            A.CallTo(() => productsService.ListAllProducts()).Returns(products);
            var vendingMachine = new VendingMachine(productsService, moneyService);

            var transaction = new Transaction {Product = product, Funds = 10m};
            vendingMachine.PurchaseProduct(transaction);

            Assert.That(product.Stock, Is.EqualTo(0));
        }

        [Test]
        public void it_should_not_allow_a_product_to_be_purchased_if_it_is_unknown()
        {
            var unknownProduct = new Product {Id = 2, Name = "fake", Stock = 1, Price = 5};
            var products = new List<Product> {A.Fake<Product>()};
            var productsService = A.Fake<IManageProducts>();
            var moneyService = A.Fake<IManageMoney>();
            A.CallTo(() => productsService.ListAllProducts()).Returns(products);
            var vendingMachine = new VendingMachine(productsService, moneyService);

            var transaction = new Transaction {Product = unknownProduct, Funds = 10m};
            var result = vendingMachine.PurchaseProduct(transaction);

            Assert.That(result.Success, Is.False);
            Assert.That(result.Error, Is.EqualTo("Sorry. The item you have selected is unknown."));
        }

        [Test]
        public void it_should_not_allow_a_product_to_be_purchased_if_it_is_out_of_stock()
        {
            var product = new Product {Id = 2, Name = "fake", Stock = 0, Price = 5};
            var products = new List<Product> {product};
            var productsService = A.Fake<IManageProducts>();
            var moneyService = A.Fake<IManageMoney>();
            A.CallTo(() => productsService.ListAllProducts()).Returns(products);
            var vendingMachine = new VendingMachine(productsService, moneyService);

            var transaction = new Transaction {Product = product, Funds = 10m};
            var result = vendingMachine.PurchaseProduct(transaction);

            Assert.That(result.Success, Is.False);
            Assert.That(result.Error, Is.EqualTo("Sorry. The item you have selected is out of stock."));
        }

        [Test, Ignore("functionality not implemented yet")]
        public void it_should_not_allow_a_product_to_be_purchased_if_too_little_money_is_given()
        {
            var product = new Product {Id = 2, Name = "fake", Stock = 1, Price = 5};
            var products = new List<Product> {product};
            var productsService = A.Fake<IManageProducts>();
            var moneyService = A.Fake<IManageMoney>();
            A.CallTo(() => productsService.ListAllProducts()).Returns(products);
            var vendingMachine = new VendingMachine(productsService, moneyService);

            var transaction = new Transaction {Product = product, Funds = 1m};
            var result = vendingMachine.PurchaseProduct(transaction);

            Assert.That(result.Success, Is.False);
            Assert.That(result.Error, Is.EqualTo("Sorry. You have not inserted enough coins"));
        }

        [Test]
        public void it_should_return_a_list_of_allowed_coins()
        {
            var coins = new List<Coin>
            {
                A.Fake<Coin>(),
                A.Fake<Coin>(),
                A.Fake<Coin>()
            };
            var productsService = A.Fake<IManageProducts>();
            var moneyService = A.Fake<IManageMoney>();
            A.CallTo(() => moneyService.GetAllowedCoins()).Returns(coins);
            var vendingMachine = new VendingMachine(productsService, moneyService);

            var result = vendingMachine.ListAllowedCoins();

            Assert.That(result, Is.EqualTo(coins));
        }
    }
}
