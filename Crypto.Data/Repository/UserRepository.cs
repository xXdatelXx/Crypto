using Crypto.Data.Models;
using Microsoft.EntityFrameworkCore;
using Crypto.Data.Interface;
using System.Xml.Linq;

namespace Crypto.Data.Repository;

public class UserRepository : IUserRepository
{
    private readonly DbSet<User> _set;
    private readonly CryptoDBContext _dbContext;
    
    public UserRepository(CryptoDBContext dbContext) {
        _dbContext = dbContext;
        _set = _dbContext.Set<User>();
    }

    public async Task Create(User model, CancellationToken token)
    {
        await _set.AddAsync(model, token);
        await _dbContext.SaveChangesAsync(token);
    }

    public async Task<User?> GetAsync(Guid id, CancellationToken token) => 
        await _set.FindAsync(id, token) ?? null;

    public async Task<User?> GetByTGIdAsync(string id, CancellationToken token) {
        return await _set.FirstOrDefaultAsync(x => x.TelegramId == id, token);
    }
    
    public Task<IEnumerable<User>?> GetAll(CancellationToken token) {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(User model, CancellationToken token) {
        var old = await GetAsync(model.Id, token);
        
        if (old == null)
            throw new Exception("User not found");
        
        _dbContext.Entry(old).CurrentValues.SetValues(model);
        _dbContext.Entry(old).State = EntityState.Modified;
        
        await _dbContext.SaveChangesAsync(token);
    }
        
    public async Task<bool> CheckDoublingAsync(User model, CancellationToken token) {
        return await Task.FromResult(_set.FirstOrDefault(e => 
            e.Id != model.Id &&  e.TelegramId == model.TelegramId &&  
            e.ByBitApiKey == model.ByBitApiKey /*&&*/
            /*e.Currencies.Distinct().Count() == model.Currencies.Distinct().Count()*/) == null);
    }

    public async Task DeleteAsync(User model, CancellationToken token)
    {
        model.Removed = true;
        await UpdateAsync(model, token);
    }
}