namespace Crypto.Queries.Model;

public class WalletModel {
   public List<(string, float)> Assets { get; set; }
   public float Total => Assets.Sum(b => b.Item2);
}