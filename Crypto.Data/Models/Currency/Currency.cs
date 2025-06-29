using Crypto.Data.Interface;

namespace Crypto.Data.Models;

public class Currency : IRemovable {
   public required Guid Id { get; init; }
   public required string Name { get; init; }
   public virtual ICollection<User>? Users { get; set; }
   public bool Removed { get; set; }
}