using Crypto.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Data.Repository
{
    public class UserCurrencyRepository
    {
       /* private readonly DbSet<User_Currency> _set;

        public UserCurrencyRepository(DbSet<User_Currency> set)
        {
            _set = set;
        }

        public async Task Create(User_Currency model)
        {
            await _set.AddAsync(model);
        }

        public async void Get(Guid id)
        {
            var model = await _set.FindAsync(id);
           // return await _set.FindAsync(id);
        }*/

       /* public async Task Update(User user)
        {
            var model = await Get(user.Id);
            _set.Update(model);
        }

        public bool CheckDoubling(User user)
        {
            return _set.FirstOrDefault(e =>
                //!e.IsRemoved &&
                e.Id != user.Id &&
                e.TelegramId == user.TelegramId &&
                e.ByBitApiKey == user.ByBitApiKey &&
                e.Currencies.Distinct().Count() == e.Currencies.Count) == null;
        }*/
    }
}
