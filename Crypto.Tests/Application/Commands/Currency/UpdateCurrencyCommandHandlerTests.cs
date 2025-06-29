using Crypto.Application.Logic.Commands.Currency.Update;
using Crypto.Application.Model;
using Crypto.Data.Interface;
using Crypto.Data.Models;
using FluentValidation;
using Moq;
using NUnit.Framework;

namespace Crypto.Tests.Application.Commands;

public class UpdateCurrencyCommandHandlerTests {
   private Mock<ICurrencyRepository> _currencyRepoMock;
   private UpdateCurrencyCommandHandler _handler;
   private Mock<IUserRepository> _userRepoMock;

   [SetUp]
   public void SetUp() {
      _currencyRepoMock = new Mock<ICurrencyRepository>();
      _userRepoMock = new Mock<IUserRepository>();
      _handler = new UpdateCurrencyCommandHandler(_currencyRepoMock.Object, _userRepoMock.Object);
   }

   [Test]
   public async Task Handle_ShouldUpdateCurrency_WhenValid() {
      var id = Guid.NewGuid();

      var currency = new CurrencyRequest {
         Id = id,
         Name = "ETH",
         Users = new List<string> { "123456789", "987654321" }
      };
      var command = new UpdateCurrencyCommand(currency);

      var existingCurrency = new Currency {
         Id = id,
         Name = "OldName",
         Users = new List<User>()
      };

      var user1 = new User { TelegramId = "123456789" };
      var user2 = new User { TelegramId = "987654321" };

      _currencyRepoMock
         .Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
         .ReturnsAsync(existingCurrency);

      _userRepoMock
         .Setup(r => r.GetByTGIdAsync("123456789", It.IsAny<CancellationToken>()))
         .ReturnsAsync(user1);

      _userRepoMock
         .Setup(r => r.GetByTGIdAsync("987654321", It.IsAny<CancellationToken>()))
         .ReturnsAsync(user2);

      _currencyRepoMock
         .Setup(r => r.UpdateAsync(existingCurrency, It.IsAny<CancellationToken>()))
         .Returns(Task.CompletedTask);

      CurrencyRequest result = await _handler.Handle(command, CancellationToken.None);

      Assert.Multiple(() => {
         Assert.That(result, Is.Not.Null);
         Assert.That(result.Id, Is.EqualTo(id));
         Assert.That(result.Name, Is.EqualTo("ETH"));
      });
   }

   [Test]
   public void Handle_ShouldThrowValidationException_WhenInvalid() {
      var id = Guid.NewGuid();
      var currency = new CurrencyRequest {
         Id = id
      };
      var command = new UpdateCurrencyCommand(currency);

      var ex = Assert.ThrowsAsync<ValidationException>(async () =>
         await _handler.Handle(command, CancellationToken.None));

      Assert.That(ex.Errors, Is.Not.Empty);
   }
}