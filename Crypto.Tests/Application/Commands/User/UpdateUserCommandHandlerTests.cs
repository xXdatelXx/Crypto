using Crypto.Application.Logic.Commands.User.Update;
using Crypto.Application.Model;
using Crypto.Data.Interface;
using Crypto.Data.Models;
using FluentValidation;
using Moq;
using NUnit.Framework;

namespace Crypto.Tests.Application.Commands;

public class UpdateUserCommandHandlerTests {
   private Mock<ICurrencyRepository> _currencyRepoMock;
   private UpdateUserCommandHandler _handler;
   private Mock<IUserRepository> _userRepoMock;

   [SetUp]
   public void SetUp() {
      _userRepoMock = new Mock<IUserRepository>();
      _currencyRepoMock = new Mock<ICurrencyRepository>();
      _handler = new UpdateUserCommandHandler(_userRepoMock.Object, _currencyRepoMock.Object);
   }

   [Test]
   public async Task Handle_ShouldUpdateUser_WhenValid() {
      var id = Guid.NewGuid();
      var currencies = new List<string> { "BTC", "ETH" };
      var command = new UpdateUserCommand(new UserRequest {
         Id = id,
         TelegramId = "123456789",
         ByBitApiKey = "key",
         ByBitApiSicret = "secret",
         Currencies = currencies
      });

      var user = new User {
         Id = id,
         Currencies = new List<Currency>()
      };

      var btc = new Currency { Name = "BTC", Users = new List<User>() };
      var eth = new Currency { Name = "ETH", Users = new List<User>() };

      _userRepoMock.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
         .ReturnsAsync(user);

      _currencyRepoMock.Setup(r => r.GetByNameAsync("BTC", It.IsAny<CancellationToken>()))
         .ReturnsAsync(btc);

      _currencyRepoMock.Setup(r => r.GetByNameAsync("ETH", It.IsAny<CancellationToken>()))
         .ReturnsAsync(eth);

      _currencyRepoMock.Setup(r => r.UpdateAsync(It.IsAny<Currency>(), It.IsAny<CancellationToken>()))
         .Returns(Task.CompletedTask);

      _userRepoMock.Setup(r => r.UpdateAsync(user, It.IsAny<CancellationToken>()))
         .Returns(Task.CompletedTask);

      var result = await _handler.Handle(command, CancellationToken.None);

      Assert.Multiple(() => {
         Assert.That(result.Id, Is.EqualTo(id));
         Assert.That(result.TelegramId, Is.EqualTo("123456789"));
         Assert.That(result.Currencies, Is.EquivalentTo(currencies));
      });
   }

   [Test]
   public void Handle_ShouldThrowValidationException_WhenInvalid() {
      var id = Guid.NewGuid();
      var command = new UpdateUserCommand(new UserRequest {
         Id = id
      });

      var ex = Assert.ThrowsAsync<ValidationException>(async () =>
         await _handler.Handle(command, CancellationToken.None));

      Assert.That(ex.Errors, Is.Not.Empty);
   }
}