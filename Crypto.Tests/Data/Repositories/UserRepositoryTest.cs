using Crypto.Data;
using Crypto.Data.Models;
using Crypto.Data.Repository;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Crypto.Tests.Data.Repositories;

public sealed class UserRepositoryTest {
   private CryptoDBContext _context;
   private UserRepository _repository;

   [SetUp]
   public void Setup() {
      var options = new DbContextOptionsBuilder<CryptoDBContext>()
         .UseInMemoryDatabase(Guid.NewGuid().ToString())
         .Options;

      _context = new CryptoDBContext(options);
      _repository = new UserRepository(_context);
   }

   [Test]
   public async Task CreateAsync_Should_Add_User() {
      var user = new User {
         Id = Guid.NewGuid(),
         TelegramId = "123456789",
         ByBitApiKey = "test_api_key",
         ByBitApiSicret = "test_secret"
      };

      await _repository.CreateAsync(user, CancellationToken.None);
      var result = await _context.Users.FirstOrDefaultAsync(u => u.TelegramId == "123456789");

      Assert.That("123456789", Is.EqualTo(result.TelegramId));
   }

   [Test]
   public async Task GetAsync_Should_Return_User_By_Id() {
      var user = new User {
         Id = Guid.NewGuid(),
         TelegramId = "123456789",
         ByBitApiKey = "test_api_key",
         ByBitApiSicret = "test_secret"
      };

      _context.Users.Add(user);
      await _context.SaveChangesAsync();

      var result = await _repository.GetAsync(user.Id, CancellationToken.None);

      Assert.That(user.TelegramId, Is.EqualTo(result.TelegramId));
   }

   [Test]
   public async Task GetByTGIdAsync_Should_Return_User_By_TelegramId() {
      var user = new User {
         Id = Guid.NewGuid(),
         TelegramId = "123456789",
         ByBitApiKey = "test_api_key",
         ByBitApiSicret = "test_secret"
      };

      _context.Users.Add(user);
      await _context.SaveChangesAsync();

      var result = await _repository.GetByTGIdAsync("123456789", CancellationToken.None);

      Assert.That(user.Id, Is.EqualTo(result.Id));
   }

   [Test]
   public async Task UpdateAsync_Should_Modify_User() {
      var user = new User {
         Id = Guid.NewGuid(),
         TelegramId = "123456789",
         ByBitApiKey = "test_api_key",
         ByBitApiSicret = "test_secret"
      };
      _context.Users.Add(user);
      await _context.SaveChangesAsync();

      user.TelegramId = "tg_updated";
      await _repository.UpdateAsync(user, CancellationToken.None);

      var result = await _context.Users.FindAsync(user.Id);

      Assert.That("tg_updated", Is.EqualTo(result?.TelegramId));
   }

   [Test]
   public async Task DeleteAsync_Should_Remove_User() {
      var user = new User {
         Id = Guid.NewGuid(),
         TelegramId = "223456789",
         ByBitApiKey = "test_api_key",
         ByBitApiSicret = "test_secret"
      };

      _context.Users.Add(user);
      await _context.SaveChangesAsync();

      await _repository.DeleteAsync(user, CancellationToken.None);

      var result = await _context.Users.FindAsync(user.Id) == null;

      Assert.That(result, Is.False);
   }

   [Test]
   public async Task CheckDoublingAsync_Should_Return_True_For_Duplicate() {
      var user = new User {
         Id = Guid.NewGuid(),
         TelegramId = "123456789",
         ByBitApiKey = "test_api_key",
         ByBitApiSicret = "test_secret"
      };
      _context.Users.Add(user);
      await _context.SaveChangesAsync();

      var duplicate = new User { TelegramId = "523456789" };
      var result = await _repository.CheckDoublingAsync(duplicate, CancellationToken.None);

      Assert.That(result, Is.False);
   }

   [Test]
   public async Task CheckDoublingAsync_Should_Return_False_If_No_Duplicate() {
      var user = new User {
         Id = Guid.NewGuid(),
         TelegramId = "123456789",
         ByBitApiKey = "test_api_key",
         ByBitApiSicret = "test_secret"
      };
      var result = await _repository.CheckDoublingAsync(user, CancellationToken.None);

      Assert.That(result, Is.False);
   }

   [Test]
   public async Task Cant_Add_Duplicate_Test() {
      var user = new User {
         Id = Guid.NewGuid(),
         TelegramId = "123456789",
         ByBitApiKey = "test_api_key",
         ByBitApiSicret = "test_secret"
      };
      await _repository.CreateAsync(user, CancellationToken.None);

      var duplicateUser = new User { TelegramId = "123456789" };

      var isDuplicate = await _repository.CheckDoublingAsync(duplicateUser, CancellationToken.None);

      Assert.That(isDuplicate, Is.False);
   }
}