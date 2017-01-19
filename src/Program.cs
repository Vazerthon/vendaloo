﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using Vendaloo.Contracts;
using Vendaloo.Models;

namespace Vendaloo
{
    class Program
    {
        static IServiceProvider serviceProvider = Startup.GetProvider();
        static readonly IVendingMachine VendingMachine = serviceProvider.GetService<IVendingMachine>();

        static void Main()
        {
            var vend = 'y';
            while (vend == 'y')
            {
                var products = VendingMachine.ListProducts().ToList();
                PrintProductList(products);
                var product = GetProductSelection(products);
                var money = GetMoney(product);
                MakeTransaction(product, money);
                vend = PrintExitMessage();
            }
        }

        static decimal GetMoney(Product product)
        {
            var total = 0M;
            var coins = VendingMachine.ListAllowedCoins().ToList();

            while (total < product.Price)
            {
                var more = total > 0 ? "more " : "";
                Console.WriteLine($"Please insert {more}coins ({total.ToString("C")})");
                var input = Console.ReadLine();
                decimal value;
                if (decimal.TryParse(input, out value))
                {
                    if (coins.Select(c => c.Value).Contains(value))
                    {
                        total += value;
                    }
                    else
                    {
                        Error($"Only the following coins are allowed: {string.Join(", ", coins.Select(d => d.AsCurrency))}");
                    }
                }
                else
                {
                    Error("Sorry. That input is invalid, please try again");
                }
            }

            return total;
        }

        static void MakeTransaction(Product product, decimal money)
        {
            var result = VendingMachine.PurchaseProduct(new Transaction { Product = product, Funds = money });

            if (result.Success)
            {
                Console.WriteLine("Thank you for your purchase");
                PrintChange(result.Change.ToList());
                Console.WriteLine("\n");
                return;
            }

            Error(result.Error);
        }

        static void PrintChange(IList<Coin> coins)
        {
            if (!coins.Any())
            {
                return;
            }

            Console.WriteLine($"Your change is: {string.Join(", ", coins.Select(d => d.AsCurrency))}");
        }

        static Product GetProductSelection(IList<Product> products)
        {
            var selectedProductId = -1;
            while (selectedProductId < 0)
            {
                Console.WriteLine("Please select a product...");
                var input = Console.ReadLine();
                int parsed;
                if (int.TryParse(input, out parsed))
                {
                    selectedProductId = parsed;
                }
                else
                {
                    Error("Sorry. That input is invalid, please try again");
                    Console.WriteLine("\n");
                }
            }

            var product = products.SingleOrDefault(p => p.Id.Equals(selectedProductId));
            if (product != null)
            {
                return product;
            }
            Error($"Sorry. No product found with an ID of {selectedProductId}, Please try again");
            GetProductSelection(products);
            return null;
        }

        static char PrintExitMessage()
        {
            Console.WriteLine("Continue? (y/n)");
            var input = Console.ReadKey().KeyChar;
            Console.WriteLine("\n");
            return input;
        }

        static void PrintProductList(IEnumerable<Product> products)
        {
            Console.WriteLine("╔═══════════════════════════════════════════════════════════════╗");
            Console.WriteLine($"║ #\t║\tProduct\t\t║\tPrice\t║\tStock\t║");
            Console.WriteLine("║                                                               ║");
            foreach (var product in products)
            {
                Console.WriteLine($"║ {product.Id}\t║\t{product.Name.PadRight(12, ' ')}\t║\t{product.Price.ToString("C")}\t║\t{product.Stock}\t║");
            }
            Console.WriteLine("╚═══════════════════════════════════════════════════════════════╝");
            Console.WriteLine("\n");
        }

        static void Error(string message)
        {
            Console.WriteLine(message);
        }
    }
}
