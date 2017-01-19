using System.Collections.Generic;
using NUnit.Framework;
using Vendaloo.Contracts;
using Vendaloo.Models;
using Vendaloo.Services;
using NSubstitute;

namespace Vendaloo.Tests
{
    [TestFixture]
    public class when_using_a_vending_machine
    {
        [Test]
        public void it_should_be_able_to_produce_a_list_of_products()
        {
            var products = new List<Product> {  new Product(0, "", 0, 0),  new Product(0, "", 0, 0) };
            var productsService = Substitute.For<IManageProducts>();
            var moneyService = Substitute.For<IManageMoney>();
            productsService.ListAllProducts().Returns(products);

            var vendingMachine = new VendingMachine(productsService, moneyService);

            var result = vendingMachine.ListProducts();

            Assert.That(result, Is.EqualTo(products));
        }

        [Test]
        public void it_should_allow_a_product_to_be_purchased()
        {
            var product = new Product(2, "fake", 1, 5);
            var products = new List<Product> {product};
            var productsService = Substitute.For<IManageProducts>();
            var moneyService = Substitute.For<IManageMoney>();
            productsService.ListAllProducts().Returns(products);

            var vendingMachine = new VendingMachine(productsService, moneyService);

            var transaction = new Transaction {Product = product, Funds = 10m};
            var result = vendingMachine.PurchaseProduct(transaction);

            Assert.That(result.Success, Is.True);
        }

        [Test]
        public void it_should_reduce_the_stock_count_after_a_transaction()
        {
            var product = new Product(2, "fake", 1, 1);
            var products = new List<Product> {product};
            var productsService = Substitute.For<IManageProducts>();
            var moneyService = Substitute.For<IManageMoney>();
            productsService.ListAllProducts().Returns(products);
            var vendingMachine = new VendingMachine(productsService, moneyService);

            var transaction = new Transaction {Product = product, Funds = 10m};
            vendingMachine.PurchaseProduct(transaction);

            Assert.That(product.Stock, Is.EqualTo(0));
        }

        [Test]
        public void it_should_not_allow_a_product_to_be_purchased_if_it_is_unknown()
        {
            var unknownProduct = new Product(2, "fake", 1, 5);
            var products = new List<Product> {  new Product(0, "", 0, 0) };
            var productsService = Substitute.For<IManageProducts>();
            var moneyService = Substitute.For<IManageMoney>();
            productsService.ListAllProducts().Returns(products);
            var vendingMachine = new VendingMachine(productsService, moneyService);

            var transaction = new Transaction {Product = unknownProduct, Funds = 10m};
            var result = vendingMachine.PurchaseProduct(transaction);

            Assert.That(result.Success, Is.False);
            Assert.That(result.Error, Is.EqualTo("Sorry. The item you have selected is unknown."));
        }

        [Test]
        public void it_should_not_allow_a_product_to_be_purchased_if_it_is_out_of_stock()
        {
            var product = new Product(2, "fake", 1, 0);
            var products = new List<Product> { product };
            var productsService = Substitute.For<IManageProducts>();
            var moneyService = Substitute.For<IManageMoney>();
            productsService.ListAllProducts().Returns(products);
            var vendingMachine = new VendingMachine(productsService, moneyService);

            var transaction = new Transaction {Product = product, Funds = 10m};
            var result = vendingMachine.PurchaseProduct(transaction);

            Assert.That(result.Success, Is.False);
            Assert.That(result.Error, Is.EqualTo("Sorry. The item you have selected is out of stock."));
        }

        [Test]
        public void it_should_not_allow_a_product_to_be_purchased_if_too_little_money_is_given()
        {
            var product = new Product(2, "fake", 2, 5);
            var products = new List<Product> { product };
            var productsService = Substitute.For<IManageProducts>();
            var moneyService = Substitute.For<IManageMoney>();
            productsService.ListAllProducts().Returns(products);
            var vendingMachine = new VendingMachine(productsService, moneyService);

            var transaction = new Transaction {Product = product, Funds = 1m};
            var result = vendingMachine.PurchaseProduct(transaction);

            Assert.That(result.Success, Is.False);
            Assert.That(result.Error, Is.EqualTo("Sorry. You have not inserted enough coins."));
        }

        [Test]
        public void it_should_return_a_list_of_allowed_coins()
        {
            var coins = new List<Coin>
            {
                new Coin(0.01m),
                new Coin(0.02m),
                new Coin(0.05m),
            };
            var productsService = Substitute.For<IManageProducts>();
            var moneyService = Substitute.For<IManageMoney>();
            moneyService.GetAllowedCoins().Returns(coins);
            var vendingMachine = new VendingMachine(productsService, moneyService);

            var result = vendingMachine.ListAllowedCoins();

            Assert.That(result, Is.EqualTo(coins));
        }

        [Test]
        public void it_should_return_change_after_a_successful_transaction()
        {
            var coins = new List<Coin>
            {
                new Coin(0.01m),
                new Coin(0.02m),
                new Coin(0.05m),
            };

            var product = new Product(2, "fake", 1, 5);
            var products = new List<Product> { product };
            var productsService = Substitute.For<IManageProducts>();
            var moneyService = Substitute.For<IManageMoney>();
            productsService.ListAllProducts().Returns(products);
            moneyService.GetValueAsCoins(Arg.Any<decimal>()).Returns(coins);
            var vendingMachine = new VendingMachine(productsService, moneyService);

            var transaction = new Transaction { Product = product, Funds = 10m };
            var result = vendingMachine.PurchaseProduct(transaction);

            Assert.That(result.Success, Is.True);
            Assert.That(result.Change, Is.EqualTo(coins));
        }

        [Test]
        public void it_should_request_change_for_the_correct_value()
        {
            var product = new Product(2, "fake", 1, 5);
            var products = new List<Product> { product };
            var productsService = Substitute.For<IManageProducts>();
            var moneyService = Substitute.For<IManageMoney>();
            productsService.ListAllProducts().Returns(products);
            var vendingMachine = new VendingMachine(productsService, moneyService);

            var transaction = new Transaction { Product = product, Funds = 10m };
            vendingMachine.PurchaseProduct(transaction);

            moneyService.GetValueAsCoins(transaction.Funds - product.Price).Received(1);
        }
    }
}
