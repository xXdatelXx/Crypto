using Crypto.Data.Models;

namespace Crypto.Data.Interface;

public interface IUserRepository {
   Task CreateAsync(User model, CancellationToken token);
   Task<User?> GetAsync(Guid id, CancellationToken token);
   Task<User?> GetByTGIdAsync(string id, CancellationToken token);
   Task<IEnumerable<User>?> GetAllAsync(CancellationToken token);
   Task UpdateAsync(User model, CancellationToken token);
   Task DeleteAsync(User model, CancellationToken token);
   Task<bool> CheckDoublingAsync(User model, CancellationToken token);
}