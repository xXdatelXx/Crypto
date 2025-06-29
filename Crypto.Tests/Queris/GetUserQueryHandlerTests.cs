using Crypto.Data.Interface;
using Crypto.Data.Models;
using Crypto.Queries.Model;
using Crypto.Queries.Queries.User;
using Moq;
using NUnit.Framework;

namespace Crypto.Tests.Queris;

public class GetUserQueryHandlerTests {
   private GetUserQueryHandler _handler;
   private Mock<IUserRepository> _userRepositoryMock;

   [SetUp]
   public void SetUp() {
      _userRepositoryMock = new Mock<IUserRepository>();
      _handler = new GetUserQueryHandler(_userRepositoryMock.Object);
   }

   [Test]
   public async Task Handle_ReturnsUserModel_WhenUserExists() {
      var telegramId = "123456789";
      var user = new User {
         TelegramId = telegramId,
         ByBitApiKey = "test-api-key",
         ByBitApiSicret = "test-api-secret",
         Currencies = new List<Currency> {
            new() { Name = "BTC" },
            new() { Name = "ETH" }
         }
      };

      _userRepositoryMock
         .Setup(r => r.GetByTGIdAsync(telegramId, It.IsAny<CancellationToken>()))
         .ReturnsAsync(user);

      var query = new GetUserByTGIdQuery(telegramId);

      UserResponse result = await _handler.Handle(query, CancellationToken.None);

      Assert.Multiple(() => {
         Assert.That(result.Id, Is.EqualTo(user.Id));
         Assert.That(result.TelegramId, Is.EqualTo(user.TelegramId));
         Assert.That(result.ByBitApiKey, Is.EqualTo(user.ByBitApiKey));
         Assert.That(result.ByBitApiSecret, Is.EqualTo(user.ByBitApiSicret));
         Assert.That(result.Currencies, Is.EquivalentTo(user.Currencies.Select(c => c.Name).ToList()));
      });
   }
}