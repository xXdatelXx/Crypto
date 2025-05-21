using Crypto.Data.Interface;

namespace Crypto.Data.Models;

public class Currency : IRemovable {
   public Guid Id { get; set; }
   public string Name { get; set; }
   public Guid? UserCurrencyId { get; set; } = Guid.Empty;
   public bool Removed { get; set; }
   public virtual ICollection<User> Users { get; set; }
}