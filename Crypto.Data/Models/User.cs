using System.ComponentModel.DataAnnotations;
using Crypto.Data.Interface;

namespace Crypto.Data.Models;

public class User : IRemovable {
   [Key] public Guid Id { get; set; }
   public string TelegramId { get; set; }
   public string ByBitApiKey { get; set; }
   public string ByBitApiSicret { get; set; }
   public virtual ICollection<Currency> Currencies { get; set; }
   public bool Removed { get; set; }
}