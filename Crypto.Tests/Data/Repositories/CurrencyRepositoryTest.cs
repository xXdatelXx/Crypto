using Crypto.Data;
using Crypto.Data.Models;
using Crypto.Data.Repository;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Crypto.Tests.Data.Repositories;

public sealed class CurrencyRepositoryTest {
   private CryptoDBContext _context;
   private CurrencyRepository _repository;

   [SetUp]
   public void Setup() {
      var options = new DbContextOptionsBuilder<CryptoDBContext>()
         .UseInMemoryDatabase(Guid.NewGuid().ToString())
         .Options;

      _context = new CryptoDBContext(options);
      _repository = new CurrencyRepository(_context);
   }

   [Test]
   public async Task CreateAsync_Should_Add_Currency() {
      var currency = new Currency {
         Id = Guid.NewGuid(),
         Name = "Bitcoin"
      };

      await _repository.CreateAsync(currency, CancellationToken.None);
      var result = await _context.Currencies.FirstOrDefaultAsync(c => c.Name == "Bitcoin");

      Assert.That("Bitcoin", Is.EqualTo(result?.Name));
   }

   [Test]
   public async Task GetAsync_Should_Return_Currency_By_Id() {
      var currency = new Currency {
         Id = Guid.NewGuid(),
         Name = "Ethereum"
      };

      _context.Currencies.Add(currency);
      await _context.SaveChangesAsync();

      var result = await _repository.GetByIdAsync(currency.Id, CancellationToken.None);

      Assert.That("Ethereum", Is.EqualTo(result?.Name));
   }

   [Test]
   public async Task GetByNameAsync_Should_Return_Currency_By_Name() {
      var currency = new Currency {
         Id = Guid.NewGuid(),
         Name = "Solana"
      };

      _context.Currencies.Add(currency);
      await _context.SaveChangesAsync();

      var result = await _repository.GetByNameAsync("Solana", CancellationToken.None);

      Assert.That(currency.Id, Is.EqualTo(result?.Id));
   }

   [Test]
   public async Task UpdateAsync_Should_Modify_Currency() {
      var currency = new Currency {
         Id = Guid.NewGuid(),
         Name = "Litecoin"
      };

      _context.Currencies.Add(currency);
      await _context.SaveChangesAsync();

      currency.Name = "LitecoinUpdated";
      await _repository.UpdateAsync(currency, CancellationToken.None);

      var result = await _context.Currencies.FindAsync(currency.Id);

      Assert.That("LitecoinUpdated", Is.EqualTo(result?.Name));
   }

   [Test]
   public async Task CheckDoublingAsync_Should_Return_True_For_Duplicate() {
      var currency = new Currency {
         Id = Guid.NewGuid(),
         Name = "Polkadot"
      };

      _context.Currencies.Add(currency);
      await _context.SaveChangesAsync();

      var duplicate = new Currency { Name = "Polkadot" };
      var result = await _repository.CheckDoublingAsync(duplicate, CancellationToken.None);

      Assert.That(result, Is.True);
   }

   [Test]
   public async Task CheckDoublingAsync_Should_Return_False_If_Not_Duplicated() {
      var newCurrency = new Currency { Name = "UniqueCoin" };

      var result = await _repository.CheckDoublingAsync(newCurrency, CancellationToken.None);

      Assert.That(result, Is.False);
   }
}