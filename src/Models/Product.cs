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
            Id = id;
            Name = name;
            Price = price;
            Stock = stock;
        }

        public void ReduceStockBy(int number)
        {
            Stock = (Stock - number) > 0 ? (Stock - number) : 0;
        }
    }
}
