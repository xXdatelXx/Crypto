using Crypto.Data.Interface;

namespace Crypto.Data.Models;

public sealed class Currency : ISoftDeleted {
   public required Guid Id { get; init; }
   public required string Name { get; init; }
   public ICollection<User>? Users { get; set; }
   public bool SoftDeleted { get; set; }
}