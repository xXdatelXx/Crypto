using FluentValidation;

namespace Crypto.Application.Logic.Commands;

public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand> {
   public CreateUserCommandValidator() {
      RuleFor(x => x.telegramId)
         .NotEmpty()
         .WithMessage("Telegram id is required.")
         .Length(9)
         .WithMessage("Telegram id must contains 9 characters.");
      RuleFor(x => x.bybitKey)
         .NotEmpty()
         .WithMessage("Bybit key is required.");
      RuleFor(x => x.bybitSicret)
         .NotNull()
         .WithMessage("Bybit sicret is required.");
   }
}