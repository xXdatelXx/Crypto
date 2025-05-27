using Crypto.Data.Interface;
using Crypto.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Crypto.Data.Repository;

public class CurrencyRepository : ICurrencyRepository {
   private readonly CryptoDBContext _dbContext;

   public CurrencyRepository(CryptoDBContext dbContext) {
      _dbContext = dbContext;
   }

   public async Task CreateAsync(Currency model, CancellationToken token) {
      await _dbContext.Currencies.AddAsync(model, token);
      await _dbContext.SaveChangesAsync(token);
   }

   public async Task<Currency?> GetAsync(Guid id, CancellationToken token) {
      return await _dbContext.Currencies.FindAsync(new object[] { id }, token);
   }

   public async Task UpdateAsync(Currency model, CancellationToken token) {
      var old = await _dbContext.Currencies.FindAsync([model.Id], token);

      if (old == null)
         throw new Exception("Currency not found");

      _dbContext.Entry(old).CurrentValues.SetValues(model);
      await _dbContext.SaveChangesAsync(token);
   }

   public async Task<bool> CheckDoublingAsync(Currency model, CancellationToken token) {
      return await _dbContext.Currencies.FirstOrDefaultAsync(e =>
         e.Id != model.Id &&
         e.Name == model.Name &&
         e.Users.Distinct().Count() == model.Users.Distinct().Count(), token) == null;
   }

   public async Task DeleteAsync(Currency model, CancellationToken token) {
      model.Removed = true;
      await UpdateAsync(model, token);
   }

   public async Task<Currency?> GetByNameAsync(string name, CancellationToken token) {
      return await _dbContext.Currencies.Include(x => x.Users).Where(i => i.Name == name).FirstOrDefaultAsync(token);
   }
}