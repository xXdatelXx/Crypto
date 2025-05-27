using Crypto.Application.Model;
using Crypto.Data.Interface;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Crypto.Application.Logic.Commands.User.Create;

public sealed class CreateUserCommandHandler(IUserRepository repository) : IRequestHandler<CreateUserCommand, UserDTO> {
   public async Task<UserDTO> Handle(CreateUserCommand request, CancellationToken cancellationToken) {
      var validator = new CreateUserCommandValidator();
      var validationResult = await validator.ValidateAsync(request, cancellationToken);
      if (!validationResult.IsValid)
         throw new ValidationException(validationResult.Errors);

      Data.Models.User user = new() {
         TelegramId = request.telegramId,
         ByBitApiKey = request.bybitKey,
         ByBitApiSicret = request.bybitSicret
      };

      if (await repository.CheckDoublingAsync(user, cancellationToken))
         throw new DbUpdateException("User is already exists");
      
      await repository.CreateAsync(user, cancellationToken);

      return new UserDTO {
         TelegramId = user.TelegramId,
         ByBitApiKey = user.ByBitApiKey,
         ByBitApiSicret = user.ByBitApiSicret
      };
   }
}