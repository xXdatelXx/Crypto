namespace Crypto.Application.Model;

public class DifferenceDto
{
    public string Symbol { get; set; }
    public DateTime Time { get; set; }
    public float OldPrice { get; set; }
    public float CurrentPrice { get; set; }
    public float Difference { get; set; }
    public double PercentChange { get; set; }
}