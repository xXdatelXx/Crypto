using Crypto.Application.Logic.Commands.User.Create;
using Crypto.Data.Interface;
using Crypto.Data.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace Crypto.Tests.Application.Commands;

public class CreateUserCommandHandlerTests {
   private CreateUserCommandHandler _handler;
   private Mock<IUserRepository> _repositoryMock;

   [SetUp]
   public void SetUp() {
      _repositoryMock = new Mock<IUserRepository>();
      _handler = new CreateUserCommandHandler(_repositoryMock.Object);
   }

   [Test]
   public async Task Handle_ShouldCreateUser_WhenValid() {
      var command = new CreateUserCommand("123456789", "key", "secret");

      _repositoryMock.Setup(r => r.CheckDoublingAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
         .ReturnsAsync(false);

      _repositoryMock.Setup(r => r.CreateAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
         .Returns(Task.CompletedTask);

      var result = await _handler.Handle(command, CancellationToken.None);

      Assert.Multiple(() => {
         Assert.That(result.TelegramId, Is.EqualTo("123456789"));
         Assert.That(result.ByBitApiKey, Is.EqualTo("key"));
         Assert.That(result.ByBitApiSicret, Is.EqualTo("secret"));
      });
   }

   [Test]
   public void Handle_ShouldThrow_WhenValidationFails() {
      var command = new CreateUserCommand("0", "", "");

      var ex = Assert.ThrowsAsync<ValidationException>(async () =>
         await _handler.Handle(command, CancellationToken.None));

      Assert.That(ex.Errors, Is.Not.Empty);
   }

   [Test]
   public void Handle_ShouldThrow_WhenUserExists() {
      var command = new CreateUserCommand("123456789", "key", "secret");

      _repositoryMock.Setup(r => r.CheckDoublingAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
         .ReturnsAsync(true);

      Assert.ThrowsAsync<DbUpdateException>(async () =>
         await _handler.Handle(command, CancellationToken.None));
   }
}