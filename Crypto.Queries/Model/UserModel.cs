namespace Crypto.Queries.Model;

public class UserModel {
   public Guid Id { get; set; }
   public string TelegramId { get; set; }
   public string ByBitApiKey { get; set; }
   public string ByBitApiSicret { get; set; }
   public IEnumerable<string> Currencies { get; set; }
}