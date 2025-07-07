using FluentValidation;

namespace Crypto.Application.Logic.Commands.Currency.Update;

public sealed class UpdateCurrencyCommandValidator : AbstractValidator<UpdateCurrencyCommand> {
   public UpdateCurrencyCommandValidator() {
      RuleFor(x => x.name)
         .NotEmpty()
         .WithMessage("Name is required.")
         .MaximumLength(10)
         .WithMessage("Name must not exceed 10 characters.");
      RuleForEach(i => i.users)
         .NotNull()
         .WithMessage("User cannot be null.")
         // Check for dublicates
         .Must((command, user) => !command.users.Except([user]).Contains(user))
         .WithMessage("User already exists in the list.");
   }
}