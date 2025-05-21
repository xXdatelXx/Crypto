using Crypto.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Data.Interface;

public interface IUserRepository
{
   Task Create(User model, CancellationToken token);
   Task<User?> GetAsync(Guid id, CancellationToken token);
   Task<User?> GetByTGIdAsync(string id, CancellationToken token);
   Task<IEnumerable<User>?> GetAll(CancellationToken token);
   Task UpdateAsync(User model, CancellationToken token);
   Task DeleteAsync(User model, CancellationToken token);
   Task<bool> CheckDoublingAsync(User model, CancellationToken token);
}