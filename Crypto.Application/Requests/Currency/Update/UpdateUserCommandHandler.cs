using Crypto.Application.Requests.Currency;
using Crypto.Application.Requests.Currency._Extensions;
using Crypto.Data.Interface;
using MediatR;

namespace Crypto.Application.Logic.Commands.Currency.Update;

public sealed class UpdateCurrencyCommandHandler(ICurrencyRepository currencyRepository, IUserRepository userRepository) : IRequestHandler<UpdateCurrencyCommand, CurrencyResponse> {
   public async Task<CurrencyResponse> Handle(UpdateCurrencyCommand request, CancellationToken cancellationToken) {
      Data.Models.Currency old = await currencyRepository.GetByIdAsync(request.id, cancellationToken);
      
      // If user does not exist, throw an exception
      ICollection<Data.Models.User> newUsers = await Task.WhenAll(request.users.Select(async i => {
         var user = await userRepository.GetByIdAsync(i, cancellationToken);

         if (user == null)
            throw new KeyNotFoundException($"User with id {i} not found");

         return user;
      }));
      
      var updated = new Data.Models.Currency {
         Id = old.Id,
         Name = old.Name,
         Users = newUsers
      };
      
      await currencyRepository.UpdateAsync(updated, cancellationToken);

      return updated.MapToResponse();
   }
}