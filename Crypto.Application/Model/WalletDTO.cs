namespace Crypto.Application.Model;

public class WalletDTO {
   public List<(string, float)> Assets { get; set; }
   public float Total => Assets.Sum(b => b.Item2);
}