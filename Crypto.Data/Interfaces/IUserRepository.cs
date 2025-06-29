using Crypto.Data.Models;

namespace Crypto.Data.Interface;

public interface IUserRepository {
   Task<bool> CreateAsync(User model, CancellationToken token = default);
   
   Task<User?> GetByIdAsync(Guid id, CancellationToken token = default);
   Task<User?> GetByTGIdAsync(string id, CancellationToken token = default);
   Task<IEnumerable<User>> GetAllAsync(CancellationToken token = default);
   
   Task<bool> UpdateAsync(User model, CancellationToken token = default);
   
   Task<bool> DeleteAsync(User model, CancellationToken token = default);
   
   Task<bool> CheckDoublingAsync(User model, CancellationToken token = default);
}