using Crypto.Queries.Model;

namespace Crypto.Application.Requests.Currency;

public class CurrencyResponse {
   public required Guid Id { get; init; }
   public required string Name { get; init; }
   public IEnumerable<UserResponse> Users { get; set; } = [];
}