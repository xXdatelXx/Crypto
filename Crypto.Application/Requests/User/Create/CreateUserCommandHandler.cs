using Crypto.Application.Logic.Commands.User.Create;
using Crypto.Application.Requests.User.Extensions;
using Crypto.Data.Interface;
using Crypto.Queries.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Crypto.Application.Logic.Commands;

public sealed class CreateUserCommandHandler(IUserRepository repository) : IRequestHandler<CreateUserCommand, UserResponse> {
   public async Task<UserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken) {
      Data.Models.User user = request.MatToUser();

      if (await repository.CheckDoublingAsync(user, cancellationToken))
         throw new DbUpdateException("User is already exists");
      
      await repository.CreateAsync(user, cancellationToken);

      return user.MapToResponse();
   }
}