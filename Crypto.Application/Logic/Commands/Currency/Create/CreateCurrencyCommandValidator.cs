using FluentValidation;

namespace Crypto.Application.Logic.Commands;

public sealed class CreateCurrencyCommandValidator : AbstractValidator<CreateCurrencyCommand> {
   public CreateCurrencyCommandValidator() {
      RuleFor(x => x.Name)
         .NotEmpty()
         .WithMessage("Name is required.")
         .MaximumLength(10)
         .WithMessage("Name must not exceed 10 characters.");
   }
}