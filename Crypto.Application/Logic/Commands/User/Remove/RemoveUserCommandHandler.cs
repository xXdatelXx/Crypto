using Crypto.Data.Interface;
using Crypto.Data.Models;
using MediatR;

namespace Crypto.Application.Logic.Commands;

public sealed class RemoveUserCommandHandler(IUserRepository repository) : IRequestHandler<RemoveUserCommand, Unit> {
   public async Task<Unit> Handle(RemoveUserCommand request, CancellationToken cancellationToken) {
      User user = await repository.GetAsync(request.id, cancellationToken);
      await repository.DeleteAsync(user, cancellationToken);

      return new Unit();
   }
}