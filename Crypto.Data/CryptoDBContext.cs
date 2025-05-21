using Crypto.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Crypto.Data;

public class CryptoDBContext(DbContextOptions<CryptoDBContext> options) : DbContext(options) {
   public DbSet<User> Users { get; set; }
   public DbSet<Currency> Currencies { get; set; }
}