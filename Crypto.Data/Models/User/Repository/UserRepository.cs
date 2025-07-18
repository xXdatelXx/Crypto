using Crypto.Data.Interface;
using Crypto.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Crypto.Data.Repository;

public sealed class UserRepository(CryptoDbContext dbContext) : IUserRepository {
   public async Task<bool> CreateAsync(User model, CancellationToken token) {
      await dbContext.Users.AddAsync(model, token);
      await dbContext.SaveChangesAsync(token);

      return true;
   }

   public async Task<User?> GetByIdAsync(Guid id, CancellationToken token) {
      return await dbContext.Users
         .Include(x => x.Currencies)
         .Where(i => i.Id == id).FirstOrDefaultAsync(token);
   }

   public async Task<User?> GetByTGIdAsync(string id, CancellationToken token) {
      return await dbContext.Users
         .Include(x => x.Currencies)
         .FirstOrDefaultAsync(x => x.TelegramId == id, token);
   }

   public Task<IEnumerable<User>> GetAllAsync(CancellationToken token) {
      return Task.FromResult<IEnumerable<User>>(dbContext.Users
         .Include(x => x.Currencies)
         .Where(i => !i.SoftDeleted));
   }

   public async Task<bool> UpdateAsync(User model, CancellationToken token) {
      var old = await dbContext.Users.FindAsync([model.Id], token);

      if (old == null)
         throw new Exception("User not found");

      dbContext.Entry(old).CurrentValues.SetValues(model);
      await dbContext.SaveChangesAsync(token);

      return true;
   }

   public async Task<bool> CheckDoublingAsync(User model, CancellationToken token) {
      return await dbContext.Set<User>().FirstOrDefaultAsync(e =>
         e.Id != model.Id && e.TelegramId == model.TelegramId &&
         e.ByBitApiKey == model.ByBitApiKey, token) != null;
   }

   public async Task<bool> DeleteAsync(User model, CancellationToken token) {
      if(await GetByIdAsync(model.Id, token) == null)
         return false;
      
      dbContext.Remove(model);
      return true;
      //model.Removed = true;
      //await UpdateAsync(model, token);
   }
}