namespace Crypto.Queris.Model;

public class DifferenceModel
{
    public string Symbol { get; set; }
    public DateTime Time { get; set; }
    public float OldPrice { get; set; }
    public float CurrentPrice { get; set; }
    public float Difference { get; set; }
    public double PercentChange { get; set; }
}