using FluentValidation;

namespace Crypto.Application.Logic.Commands;

public class CreateCurrencyCommandValidator : AbstractValidator<CreateCurrencyCommand> {
   public CreateCurrencyCommandValidator() {
      RuleFor(x => x.Name)
         .NotEmpty()
         .WithMessage("Name is required.")
         .MaximumLength(5)
         .WithMessage("Name must not exceed 50 characters.");
   }
}