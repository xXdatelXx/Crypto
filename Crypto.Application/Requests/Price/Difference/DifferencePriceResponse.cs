namespace Crypto.Queries.Model;

public class DifferencePriceResponse {
   public required string Symbol { get; init; }
   public required DateTime Time { get; init; }
   public required float OldPrice { get; init; }
   public required float CurrentPrice { get; init; }
   public required float Difference { get; init; }
   public required double PercentChange { get; init; }
}