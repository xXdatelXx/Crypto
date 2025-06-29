using Crypto.Application.Logic.Commands.User.Remove;
using Crypto.Data.Interface;
using Crypto.Data.Models;
using MediatR;
using Moq;
using NUnit.Framework;

namespace Crypto.Tests.Application.Commands;

public class RemoveUserCommandHandlerTests {
   private RemoveUserCommandHandler _handler;
   private Mock<IUserRepository> _repositoryMock;

   [SetUp]
   public void SetUp() {
      _repositoryMock = new Mock<IUserRepository>();
      _handler = new RemoveUserCommandHandler(_repositoryMock.Object);
   }

   [Test]
   public async Task Handle_ShouldRemoveUser_WhenExists() {
      var id = Guid.NewGuid();
      var user = new User { Id = id };

      _repositoryMock.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
         .ReturnsAsync(user);

      _repositoryMock.Setup(r => r.DeleteAsync(user, It.IsAny<CancellationToken>()))
         .Returns(Task.CompletedTask);

      var result = await _handler.Handle(new RemoveUserCommand(id), CancellationToken.None);

      Assert.That(result, Is.EqualTo(Unit.Value));
   }
}