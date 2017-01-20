namespace Vendaloo.Models
{
    public class Coin : IMoney
    {
        public decimal Value { get; private set; }

        public string ValueAsCurrency => Value.ToString("C");

        public Coin(decimal value)
        {
            Value = value;
        }
    }
}
