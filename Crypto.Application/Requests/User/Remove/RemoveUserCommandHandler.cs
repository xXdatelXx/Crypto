using Crypto.Data.Interface;
using MediatR;

namespace Crypto.Application.Logic.Commands.User.Remove;

public sealed class RemoveUserCommandHandler(IUserRepository repository) : IRequestHandler<RemoveUserCommand, bool> {
   public async Task<bool> Handle(RemoveUserCommand request, CancellationToken cancellationToken) {
      Data.Models.User? user = await repository.GetByIdAsync(request.id, cancellationToken);
      return user != null && await repository.DeleteAsync(user, cancellationToken);
   }
}