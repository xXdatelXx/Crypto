using Crypto.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Crypto.Data;

/// <summary>
/// Return only not soft deleted entities.
/// </summary>
public class CryptoDbContext(DbContextOptions<CryptoDbContext> options) : DbContext(options) {
   public DbSet<User> Users { get; set; }
   public DbSet<Currency> Currencies { get; set; }

   protected override void OnModelCreating(ModelBuilder modelBuilder) {
      base.OnModelCreating(modelBuilder);
      modelBuilder
         .Entity<User>()
         .HasQueryFilter(u => !u.SoftDeleted);
      modelBuilder
         .Entity<Currency>()
         .HasQueryFilter(c => !c.SoftDeleted);
   }
}