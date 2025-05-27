using Crypto.Application.Model;
using Crypto.Data.Interface;
using Crypto.Data.Models;
using FluentValidation;
using MediatR;

namespace Crypto.Application.Logic.Commands;

public sealed class UpdateUserCommandHandler(IUserRepository userRepository, ICurrencyRepository currencyRepository) : IRequestHandler<UpdateUserCommand, UserDTO> {
   public async Task<UserDTO> Handle(UpdateUserCommand request, CancellationToken cancellationToken) {
      var validator = new UpdateUserCommandValidator();
      var validationResult = await validator.ValidateAsync(request, cancellationToken);
      if (!validationResult.IsValid)
         throw new ValidationException(validationResult.Errors);

      var old = await userRepository.GetAsync(request.user.Id, cancellationToken);

      old.TelegramId = request.user.TelegramId;
      old.ByBitApiKey = request.user.ByBitApiKey;
      old.ByBitApiSicret = request.user.ByBitApiSicret;

      if (old.Currencies == null)
         old.Currencies = new List<Currency>();

      foreach (var name in request.user.Currencies.ToList()) {
         var currency = await currencyRepository.GetByNameAsync(name, cancellationToken);
         if (currency == null) {
            await currencyRepository.CreateAsync(new Currency { Name = name, Users = new List<User> { old } }, cancellationToken);
         }
         else {
            if (currency.Users == null || currency.Users.Count == 0)
               currency.Users = new List<User> { old };
            else
               currency.Users.Add(old);
            currencyRepository.UpdateAsync(currency, cancellationToken);
         }
      }

      var toRemove = old.Currencies?.Where(c => !request.user.Currencies.Contains(c.Name)).ToList();
      foreach (var currency in toRemove)
         old.Currencies.Remove(currency);

      userRepository.UpdateAsync(old, cancellationToken);

      return request.user;
   }
}