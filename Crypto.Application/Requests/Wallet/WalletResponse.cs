namespace Crypto.Queries.Model;

public class WalletResponse {
   public IEnumerable<(string name, float currencies)> Assets { get; init; } = [];
   public float Total { get; private init; }

   public WalletResponse() => 
      Total = Assets.Sum(x => x.currencies);
}