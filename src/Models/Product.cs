using System;

namespace Vendaloo.Models
{
    public class Product
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        public decimal Price { get; private set; }

        public int Stock { get; private set; }

        public Product(int id, string name, decimal price, int stock)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name must have a value");
            }

            if (stock < 0)
            {
                throw new ArgumentException("Stock cannot be negative");
            }

            if (price <= 0)
            {
                throw new ArgumentException("Price must be more than zero");
            }

            Id = id;
            Name = name;
            Price = price;
            Stock = stock;
        }

        public void ReduceStockBy(int number)
        {
            Stock -= number;
        }
    }
}
