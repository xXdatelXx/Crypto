using FluentValidation;

namespace Crypto.Application.Logic.Commands;

public sealed class UpdateUserCommandValidator  : AbstractValidator<UpdateUserCommand> {
   public UpdateUserCommandValidator() {
      RuleFor(x => x.user.TelegramId)
         .NotEmpty()
         .WithMessage("Telegram ID is required.")
         .Length(10)
         .WithMessage("Telegram ID must be exactly 10 characters long.");
         RuleFor(x => x.user.ByBitApiSicret)
            .NotNull()
            .WithMessage("ByBitApiSicret is required.");
         RuleFor(x => x.user.ByBitApiKey)
            .NotNull()
            .WithMessage("ByBitApiKey is required.");
         RuleForEach(x => x.user.Currencies)
            .NotNull()
            .WithMessage("Currencies cannot be null.")
            // Check for dublicates
            .Must((command, currencies) => !command.user.Currencies.Except([currencies]).Contains(currencies))
            .WithMessage("Currencies already exists in the list.");
   }
}