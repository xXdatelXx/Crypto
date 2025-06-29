using FluentValidation;

namespace Crypto.Data.Models.Validators;

public sealed class CurrencyValidator : AbstractValidator<Currency> {
    public CurrencyValidator() {
        RuleFor(c => c.Id)
            .NotEmpty()
            .WithMessage("Id cannot be empty.");
        
        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage("Name cannot be empty.")
            .MaximumLength(10)
            .WithMessage("Name cannot exceed 10 characters.");
        
        RuleFor(c => c.Users)
            .Must(users => users?.Distinct().Count() == users?.Count)
            .WithMessage("Users must not contain duplicates.");
    }
}