using Crypto.Data.Interface;
using Crypto.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Crypto.Data.Repository;

public class CurrencyRepository : ICurrencyRepository {
   private readonly CryptoDBContext _dbContext;
   private readonly DbSet<Currency> _set;

   public CurrencyRepository(CryptoDBContext dbContext) {
      _dbContext = dbContext;
      _set = _dbContext.Set<Currency>();
   }

   public async Task CreateAsync(Currency model, CancellationToken token) {
      await _set.AddAsync(model, token);
      await _dbContext.SaveChangesAsync(token);
   }

   public async Task<Currency?> GetAsync(Guid id, CancellationToken token) {
      return await _set.FindAsync(id, token) ?? null;
   }

   public async Task UpdateAsync(Currency model, CancellationToken token) {
      var old = await GetAsync(model.Id, token);

      if (old == null)
         throw new Exception("Currency not found");

      _dbContext.Entry(old).CurrentValues.SetValues(model);
      _dbContext.Entry(old).State = EntityState.Modified;

      await _dbContext.SaveChangesAsync(token);
   }

   public async Task<bool> CheckDoublingAsync(Currency model, CancellationToken token) {
      return await Task.FromResult(_set.FirstOrDefault(e =>
         /*!e.IsRemoved && */
         e.Id != model.Id &&
         e.Name == model.Name &&
         e.Users.Distinct().Count() == model.Users.Distinct().Count()) == null);
   }

   public async Task DeleteAsync(Currency model, CancellationToken token) {
      model.Removed = true;
      await UpdateAsync(model, token);
   }

   public async Task<Currency?> GetByNameAsync(string name, CancellationToken token) {
      return await _set.FirstOrDefaultAsync(x => x.Name == name, token);
   }
}