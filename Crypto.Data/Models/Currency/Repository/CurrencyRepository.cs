using Crypto.Data.Interface;
using Crypto.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Crypto.Data.Repository;

public sealed class CurrencyRepository(CryptoDBContext dbContext) : ICurrencyRepository {
   public async Task<bool> CreateAsync(Currency model, CancellationToken token) {
      await dbContext.Currencies.AddAsync(model, token);
      await dbContext.SaveChangesAsync(token);

      return true;
   }

   public async Task<Currency?> GetByIdAsync(Guid id, CancellationToken token) {
      var model = await dbContext.Currencies.FindAsync([id], token);
      return model == null || model.Removed ? null : model;
   }

   public Task<IEnumerable<Currency>> GetAllAsync() {
      return Task.FromResult<IEnumerable<Currency>>(dbContext.Currencies
         .Include(i => i.Users)
         .Where(i => !i.Removed));
   }
   
   public async Task<bool> UpdateAsync(Currency model, CancellationToken token) {
      var old = await GetByIdAsync(model.Id, token);

      if (old == null)
         return false;
      
      dbContext.Entry(old).CurrentValues.SetValues(model);
      await dbContext.SaveChangesAsync(token);

      return true;
   }

   public async Task<bool> CheckDoublingAsync(Currency model, CancellationToken token) {
      return await dbContext.Set<Currency>().FirstOrDefaultAsync(e =>
         e.Id != model.Id &&
         e.Name == model.Name, token) != null;
   }

   public async Task<bool> DeleteByIdAsync(Guid id, CancellationToken token) {
      var model = await GetByIdAsync(id, token);
      
      if (model == null)
         return false;
      
      model.Removed = true;
      await UpdateAsync(model, token);
      return true;
   }

   public async Task<Currency?> GetByNameAsync(string name, CancellationToken token) =>
      await dbContext.Currencies
         .Include(x => x.Users)
         .Where(i => i.Name == name)
         .FirstOrDefaultAsync(token);
}