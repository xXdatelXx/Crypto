using Crypto.Application.Logic.Commands.Currency.Create;
using Crypto.Application.Model;
using Crypto.Data.Interface;
using Crypto.Data.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace Crypto.Tests.Application.Commands;

public class CreateCurrencyCommandHandlerTests {
   private CreateCurrencyCommandHandler _handler;
   private Mock<ICurrencyRepository> _repositoryMock;

   [SetUp]
   public void SetUp() {
      _repositoryMock = new Mock<ICurrencyRepository>();
      _handler = new CreateCurrencyCommandHandler(_repositoryMock.Object);
   }

   [Test]
   public async Task Handle_ShouldCreateCurrency_WhenValidAndNotDuplicate() {
      var request = new CreateCurrencyCommand("BTC");
      var guid = Guid.NewGuid();

      _repositoryMock
         .Setup(r => r.CheckDoublingAsync(It.IsAny<Currency>(), It.IsAny<CancellationToken>()))
         .ReturnsAsync(false);

      _repositoryMock
         .Setup(r => r.CreateAsync(It.IsAny<Currency>(), It.IsAny<CancellationToken>()))
         .Callback<Currency, CancellationToken>((currency, _) => currency.Id = guid)
         .Returns(Task.CompletedTask);

      CurrencyRequest result = await _handler.Handle(request, CancellationToken.None);

      Assert.Multiple(() => {
         Assert.That(result, Is.Not.Null);
         Assert.That(result.Id, Is.EqualTo(guid));
         Assert.That(result.Name, Is.EqualTo("BTC"));
      });
   }

   [Test]
   public void Handle_ShouldThrowValidationException_WhenNameIsInvalid() {
      var request = new CreateCurrencyCommand("");

      var ex = Assert.ThrowsAsync<ValidationException>(async () =>
         await _handler.Handle(request, CancellationToken.None));

      Assert.That(ex.Errors, Is.Not.Empty);
   }

   [Test]
   public void Handle_ShouldThrowDbUpdateException_WhenCurrencyIsDuplicate() {
      var request = new CreateCurrencyCommand("ETH");

      _repositoryMock
         .Setup(r => r.CheckDoublingAsync(It.IsAny<Currency>(), It.IsAny<CancellationToken>()))
         .ReturnsAsync(true);

      var ex = Assert.ThrowsAsync<DbUpdateException>(async () =>
         await _handler.Handle(request, CancellationToken.None));

      Assert.That(ex.Message, Is.EqualTo("Currency is already exists"));
   }
}