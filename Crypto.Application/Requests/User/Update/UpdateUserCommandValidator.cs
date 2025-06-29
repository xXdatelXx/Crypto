using FluentValidation;

namespace Crypto.Application.Logic.Commands.User.Update;

public sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand> {
   public UpdateUserCommandValidator() {
      RuleFor(x => x.telegramId)
         .NotEmpty()
         .WithMessage("Telegram ID is required.")
         .MaximumLength(15)
         .WithMessage("Telegram ID must be exactly 15 characters long.");
      RuleFor(x => x.byBitApiSecret)
         .NotNull()
         .WithMessage("ByBitApiSecret is required.");
      RuleFor(x => x.byBitApiKey)
         .NotNull()
         .WithMessage("ByBitApiKey is required.");
      RuleForEach(x => x.currencies)
         .NotNull()
         .WithMessage("Currencies cannot be null.")
         // Check for dublicates
         .Must((command, currencies) => !command.currencies.Except([currencies]).Contains(currencies))
         .WithMessage("Currencies already exists in the list.");
   }
}