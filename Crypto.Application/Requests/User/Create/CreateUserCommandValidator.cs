using FluentValidation;

namespace Crypto.Application.Logic.Commands.User.Create;

public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand> {
   public CreateUserCommandValidator() {
      RuleFor(x => x.telegramId)
         .NotEmpty()
         .WithMessage("Telegram id is required.")
         .MaximumLength(15)
         .WithMessage("Telegram id must contains 15 characters.");
      RuleFor(x => x.bybitKey)
         .NotEmpty()
         .WithMessage("Bybit key is required.");
      RuleFor(x => x.bybitSecret)
         .NotNull()
         .WithMessage("Bybit sicret is required.");
   }
}