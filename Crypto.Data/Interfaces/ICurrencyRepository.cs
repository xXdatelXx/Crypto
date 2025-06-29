using Crypto.Data.Models;

namespace Crypto.Data.Interface;

public interface ICurrencyRepository {
   Task<bool> CreateAsync(Currency model, CancellationToken token = default);
   
   Task<Currency?> GetByIdAsync(Guid id, CancellationToken token = default);
   Task<Currency?> GetByNameAsync(string name, CancellationToken token = default);
   Task<IEnumerable<Currency>> GetAllAsync();
   
   Task<bool> UpdateAsync(Currency model, CancellationToken token = default);
   
   Task<bool> CheckDoublingAsync(Currency model, CancellationToken token = default);
   
   Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default);
}