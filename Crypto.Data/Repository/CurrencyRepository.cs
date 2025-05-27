using Crypto.Data.Interface;
using Crypto.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Crypto.Data.Repository;

public sealed class CurrencyRepository(CryptoDBContext dbContext) : ICurrencyRepository {
   public async Task CreateAsync(Currency model, CancellationToken token) {
      await dbContext.Currencies.AddAsync(model, token);
      await dbContext.SaveChangesAsync(token);
   }

   public async Task<Currency?> GetAsync(Guid id, CancellationToken token) {
      return await dbContext.Currencies.FindAsync([id], token);
   }

   public async Task UpdateAsync(Currency model, CancellationToken token) {
      var old = await dbContext.Currencies.FindAsync([model.Id], token);

      if (old == null)
         throw new Exception("Currency not found");

      dbContext.Entry(old).CurrentValues.SetValues(model);
      await dbContext.SaveChangesAsync(token);
   }

   public async Task<bool> CheckDoublingAsync(Currency model, CancellationToken token) {
      return await dbContext.Set<Currency>().FirstOrDefaultAsync(e =>
         e.Id != model.Id &&
         e.Name == model.Name, token) != null;
   }

   public async Task DeleteAsync(Currency model, CancellationToken token) {
      model.Removed = true;
      await UpdateAsync(model, token);
   }

   public async Task<Currency?> GetByNameAsync(string name, CancellationToken token) =>
      await dbContext.Currencies
         .Include(x => x.Users)
         .Where(i => i.Name == name)
         .FirstOrDefaultAsync(token);
}