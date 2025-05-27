using FluentValidation;

namespace Crypto.Application.Logic.Commands.Currency.Create;

public sealed class CreateCurrencyCommandValidator : AbstractValidator<CreateCurrencyCommand> {
   public CreateCurrencyCommandValidator() {
      RuleFor(x => x.Name)
         .NotEmpty()
         .WithMessage("Name is required.")
         .MaximumLength(10)
         .WithMessage("Name must not exceed 10 characters.");
   }
}