namespace Vendaloo.Models
{
    public class Coin
    {
        public decimal Value { get; private set; }

        public string AsCurrency => Value.ToString("C");

        public Coin(decimal value)
        {
            Value = value;
        }
    }
}
