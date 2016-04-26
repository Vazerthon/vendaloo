namespace Vendaloo.Models
{
    public class Coin
    {
        public decimal Value { get; set; }

        public string AsCurrency => Value.ToString("C");
    }
}
