using System.ComponentModel.DataAnnotations;
using Crypto.Data.Interface;

namespace Crypto.Data.Models;

public sealed class User : ISoftDeleted {
   public required Guid Id { get; init; }
   public required string TelegramId { get; set; }
   public required string ByBitApiKey { get; set; }
   public required string ByBitApiSicret { get; set; }
   public ICollection<Currency> Currencies { get; set; }
   public bool SoftDeleted { get; set; }
}