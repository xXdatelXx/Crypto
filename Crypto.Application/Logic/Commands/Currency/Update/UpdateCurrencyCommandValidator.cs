using FluentValidation;

namespace Crypto.Application.Logic.Commands;

public sealed class UpdateCurrencyCommandValidator : AbstractValidator<UpdateCurrencyCommand> {
   public UpdateCurrencyCommandValidator() {
      RuleFor(x => x.currency.Name)
         .NotEmpty()
         .WithMessage("Name is required.")
         .MaximumLength(10)
         .WithMessage("Name must not exceed 10 characters.");
      RuleForEach(i => i.currency.Users)
         .NotNull()
         .WithMessage("User cannot be null.")
         // Check for dublicates
         .Must((command, user) => !command.currency.Users.Except([user]).Contains(user))
         .WithMessage("User already exists in the list.");
   }
}