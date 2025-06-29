using System.ComponentModel.DataAnnotations;
using Crypto.Data.Interface;

namespace Crypto.Data.Models;

public class User : IRemovable {
   public required Guid Id { get; init; }
   public required string TelegramId { get; set; }
   public required string ByBitApiKey { get; set; }
   public required string ByBitApiSicret { get; set; }
   public virtual ICollection<Currency> Currencies { get; set; }
   public bool Removed { get; set; }
}