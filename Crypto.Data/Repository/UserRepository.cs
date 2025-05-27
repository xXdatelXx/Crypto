using Crypto.Data.Interface;
using Crypto.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Crypto.Data.Repository;

public class UserRepository(CryptoDBContext dbContext) : IUserRepository {
   public async Task CreateAsync(User model, CancellationToken token) {
      await dbContext.Users.AddAsync(model, token);
      await dbContext.SaveChangesAsync(token);
   }

   public async Task<User?> GetAsync(Guid id, CancellationToken token) {
      return await dbContext.Users
         .Include(x => x.Currencies)
         .Where(i => i.Id == id).FirstOrDefaultAsync(token);
   }

   public async Task<User?> GetByTGIdAsync(string id, CancellationToken token) {
      return await dbContext.Users
         .Include(x => x.Currencies)
         .FirstOrDefaultAsync(x => x.TelegramId == id, token);
   }

   public async Task UpdateAsync(User model, CancellationToken token) {
      var old = await dbContext.Users.FindAsync([model.Id], token);

      if (old == null)
         throw new Exception("User not found");

      dbContext.Entry(old).CurrentValues.SetValues(model);
      await dbContext.SaveChangesAsync(token);
   }

   public async Task<bool> CheckDoublingAsync(User model, CancellationToken token) {
      return await dbContext.Set<User>().FirstOrDefaultAsync(e =>
         e.Id != model.Id && e.TelegramId == model.TelegramId &&
         e.ByBitApiKey == model.ByBitApiKey, token) == null;
   }

   public async Task DeleteAsync(User model, CancellationToken token) {
      model.Removed = true;
      await UpdateAsync(model, token);
   }
}