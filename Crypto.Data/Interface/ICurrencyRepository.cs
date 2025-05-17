using Crypto.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Data.Interface;

public interface ICurrencyRepository
{
   Task CreateAsync(Currency model, CancellationToken token);
   Task<Currency?> GetAsync(Guid id, CancellationToken token);
   Task UpdateAsync(Currency model, CancellationToken token);
   Task<bool> CheckDoublingAsync(Currency model, CancellationToken token);
   Task DeleteAsync(Currency model, CancellationToken token);
}