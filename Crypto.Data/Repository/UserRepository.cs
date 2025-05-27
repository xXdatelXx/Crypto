using Crypto.Data.Interface;
using Crypto.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Crypto.Data.Repository;

public class UserRepository : IUserRepository {
   private readonly CryptoDBContext _dbContext;

   public UserRepository(CryptoDBContext dbContext) {
      _dbContext = dbContext;
   }

   public async Task Create(User model, CancellationToken token) {
      await _dbContext.Users.AddAsync(model, token);
      await _dbContext.SaveChangesAsync(token);
   }

   public async Task<User?> GetAsync(Guid id, CancellationToken token) {
      return await _dbContext.Users.Include(x => x.Currencies).Where(i => i.Id == id).FirstOrDefaultAsync(token);
   }

   public async Task<User?> GetByTGIdAsync(string id, CancellationToken token) {
      return await _dbContext.Users
         .Include(x => x.Currencies)
         .FirstOrDefaultAsync(x => x.TelegramId == id, token);
   }

   public Task<IEnumerable<User>?> GetAll(CancellationToken token) {
      throw new NotImplementedException();
   }

   public async Task UpdateAsync(User model, CancellationToken token) {
      var old = await _dbContext.Users.FindAsync(new object[] { model.Id }, token);

      if (old == null)
         throw new Exception("User not found");

      _dbContext.Entry(old).CurrentValues.SetValues(model);
      await _dbContext.SaveChangesAsync(token);
   }

   public async Task<bool> CheckDoublingAsync(User model, CancellationToken token) {
      return await _dbContext.Set<User>().FirstOrDefaultAsync(e =>
         e.Id != model.Id && e.TelegramId == model.TelegramId &&
         e.ByBitApiKey == model.ByBitApiKey, token) == null;
   }

   public async Task DeleteAsync(User model, CancellationToken token) {
      model.Removed = true;
      await UpdateAsync(model, token);
   }
}