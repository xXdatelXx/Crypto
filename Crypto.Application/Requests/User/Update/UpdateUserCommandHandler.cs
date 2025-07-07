using Crypto.Application.Requests.User.Extensions;
using Crypto.Data.Interface;
using Crypto.Queries.Model;
using FluentValidation;
using MediatR;

namespace Crypto.Application.Logic.Commands.User.Update;

public sealed class UpdateUserCommandHandler(IUserRepository userRepository, ICurrencyRepository currencyRepository) : IRequestHandler<UpdateUserCommand, UserResponse> {
   public async Task<UserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken) {
      var old = await userRepository.GetByIdAsync(request.id, cancellationToken);

      old.TelegramId = request.telegramId;
      old.ByBitApiKey = request.byBitApiKey;
      old.ByBitApiSicret = request.byBitApiSecret;

      foreach (var name in request.currencies) {
         var currency = await currencyRepository.GetByNameAsync(name, cancellationToken);
         if (currency == null) {
            await currencyRepository.CreateAsync(new Data.Models.Currency { Id = Guid.NewGuid() ,Name = name, Users = new List<Data.Models.User> { old } }, cancellationToken);
         }
         else {
            if (currency.Users == null || currency.Users.Count == 0)
               currency.Users = new List<Data.Models.User> { old };
            else
               currency.Users.Add(old);
            await currencyRepository.UpdateAsync(currency, cancellationToken);
         }
      }

      var toRemove = old.Currencies?.Where(c => !request.currencies.Contains(c.Name)).ToList();
      foreach (var currency in toRemove)
         old.Currencies.Remove(currency);
      
      await userRepository.UpdateAsync(old, cancellationToken);

      return old.MapToResponse();
   }
}