namespace Vendaloo.Models
{
    public interface IMoney
    {
        decimal Value { get; }

        string ValueAsCurrency { get; }
    }
}
