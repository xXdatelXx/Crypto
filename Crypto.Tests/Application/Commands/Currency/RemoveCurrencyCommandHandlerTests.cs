using Crypto.Application.Logic.Commands.Currency.Remove;
using Crypto.Data.Interface;
using Crypto.Data.Models;
using MediatR;
using Moq;
using NUnit.Framework;

namespace Crypto.Tests.Application.Commands;

public class RemoveCurrencyCommandHandlerTests {
   private RemoveCurrencyCommandHandler _handler;
   private Mock<ICurrencyRepository> _repositoryMock;

   [SetUp]
   public void SetUp() {
      _repositoryMock = new Mock<ICurrencyRepository>();
      _handler = new RemoveCurrencyCommandHandler(_repositoryMock.Object);
   }

   [Test]
   public async Task Handle_ShouldDeleteCurrency_WhenCurrencyExists() {
      var id = Guid.NewGuid();
      var currency = new Currency { Id = id, Name = "BTC" };

      _repositoryMock
         .Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
         .ReturnsAsync(currency);

      _repositoryMock
         .Setup(r => r.DeleteByIdAsync(currency, It.IsAny<CancellationToken>()))
         .Returns(Task.CompletedTask);

      var request = new RemoveCurrencyCommand(id);

      Unit result = await _handler.Handle(request, CancellationToken.None);

      Assert.That(result, Is.EqualTo(Unit.Value));
      _repositoryMock.Verify(r => r.DeleteByIdAsync(currency, It.IsAny<CancellationToken>()), Times.Once);
   }
}