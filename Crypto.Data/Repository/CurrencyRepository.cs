using Crypto.Data.Interface;
using Crypto.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Crypto.Data.Repository;

public class CurrencyRepository : ICurrencyRepository {
    private readonly DbSet<Currency> _set;
    private readonly CryptoDBContext _dbContext;
    
    public CurrencyRepository(CryptoDBContext dbContext) {
        _dbContext = dbContext;
        _set = _dbContext.Set<Currency>();
    }

    public async Task Create(Currency model, CancellationToken token)
    {
        await _set.AddAsync(model, token);
        await _dbContext.SaveChangesAsync(token);
    }

    public async Task<Currency?> Get(Guid id, CancellationToken token) => 
        await _set.FindAsync(id, token) ?? null;

    public async Task Update(Currency model, CancellationToken token) {
        var old = await Get(model.Id, token);
        
        if (old == null)
            throw new Exception("Currency not found");
        
        _dbContext.Entry(old).CurrentValues.SetValues(model);
        _dbContext.Entry(old).State = EntityState.Modified;
        
        await _dbContext.SaveChangesAsync(token);
    }
    
    public async Task<bool> CheckDoubling(Currency model, CancellationToken token) {
        return await Task.FromResult(_set.FirstOrDefault(e => 
            /*!e.IsRemoved && */
            e.Id != model.Id && 
            e.Name == model.Name &&
            e.Users.Distinct().Count() == model.Users.Distinct().Count()) == null);
    }
}