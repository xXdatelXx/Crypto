namespace Crypto.Queries.Model;

public class UserResponse {
   public required Guid Id { get; init; }
   public required string TelegramId { get; init; }
   public required string ByBitApiKey { get; init; }
   public required string ByBitApiSecret { get; init; }
   public IEnumerable<string> Currencies { get; set; } = [];
}