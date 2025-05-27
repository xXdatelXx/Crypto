using Crypto.Data.Interface;
using MediatR;

namespace Crypto.Application.Logic.Commands.User.Remove;

public sealed class RemoveUserCommandHandler(IUserRepository repository) : IRequestHandler<RemoveUserCommand, Unit> {
   public async Task<Unit> Handle(RemoveUserCommand request, CancellationToken cancellationToken) {
      Data.Models.User user = await repository.GetAsync(request.id, cancellationToken);
      await repository.DeleteAsync(user, cancellationToken);

      return new Unit();
   }
}