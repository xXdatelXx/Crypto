using Crypto.Data.Models;

namespace Crypto.Data.Interface;

public interface ICurrencyRepository {
   Task CreateAsync(Currency model, CancellationToken token);
   Task<Currency?> GetAsync(Guid id, CancellationToken token);
   Task<Currency?> GetByNameAsync(string name, CancellationToken token);
   Task UpdateAsync(Currency model, CancellationToken token);
   Task<bool> CheckDoublingAsync(Currency model, CancellationToken token);
   Task DeleteAsync(Currency model, CancellationToken token);
}