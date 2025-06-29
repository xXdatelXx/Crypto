using FluentValidation;

namespace Crypto.Data.Models.Validators;

public sealed class UserValidator : AbstractValidator<User> {
   public UserValidator() {
      RuleFor(u => u.Id)
         .NotEmpty()
         .WithMessage("Id cannot be empty.");
      
      RuleFor(u => u.TelegramId)
         .NotEmpty()
         .WithMessage("Telegram ID cannot be empty.")
         .MaximumLength(20)
         .WithMessage("Telegram ID cannot exceed 20 characters.");
      
      RuleFor(u => u.ByBitApiKey)
         .NotEmpty()
         .WithMessage("ByBit API Key cannot be empty.")
         .MaximumLength(50)
         .WithMessage("ByBit API Key cannot exceed 50 characters.");

      RuleFor(u => u.ByBitApiSicret)
         .NotEmpty()
         .WithMessage("ByBit API Secret cannot be empty.")
         .MaximumLength(50)
         .WithMessage("ByBit API Secret cannot exceed 50 characters.");

      RuleFor(u => u.Currencies)
         .NotNull()
         .WithMessage("Currencies cannot be null.")
         .Must(c => c?.Distinct().Count() == c?.Count)
         .WithMessage("Users must not contain duplicates.");
   }
}