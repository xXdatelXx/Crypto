using Crypto.Application.Logic.Commands.User.Create;
using Crypto.Data.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Crypto.Application.Logic.Commands;

public sealed class CreateUserCommandHandler(IUserRepository repository) : IRequestHandler<CreateUserCommand, Guid> {
   public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken) {
      Data.Models.User user = new() {
         Id = Guid.NewGuid(),
         TelegramId = request.telegramId,
         ByBitApiKey = request.bybitKey,
         ByBitApiSicret = request.bybitSecret
      };

      if (await repository.CheckDoublingAsync(user, cancellationToken))
         throw new DbUpdateException("User is already exists");
      
      await repository.CreateAsync(user, cancellationToken);

      return user.Id;
   }
}