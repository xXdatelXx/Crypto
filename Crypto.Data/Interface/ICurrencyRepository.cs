using Crypto.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Data.Interface;

public interface ICurrencyRepository
{
   Task Create(Currency model, CancellationToken token);
   Task<Currency?> Get(Guid id, CancellationToken token);
   Task Update(Currency model, CancellationToken token);
   Task<bool> CheckDoubling(Currency model, CancellationToken token);
}