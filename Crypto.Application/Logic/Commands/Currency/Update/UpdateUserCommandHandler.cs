using Crypto.Data.Interface;
using Crypto.Data.Models;
using MediatR;

namespace Crypto.Application.Logic.Commands;

public class UpdateCurrencyCommandHandler(ICurrencyRepository currencyRepository, IUserRepository userRepository) : IRequestHandler<UpdateCurrencyCommand, CurrencyDTO> {
   public async Task<CurrencyDTO> Handle(UpdateCurrencyCommand request, CancellationToken cancellationToken) {
      Currency old = await currencyRepository.GetAsync(request.currency.Id, cancellationToken);

      old.Id = request.currency.Id;
      old.Name = request.currency.Name;

      List<User> users = new();
      if (request.currency.Users is not null) {
         request.currency.Users?.ToList().ForEach(async c => {
            var u = await userRepository.GetByTGIdAsync(c, cancellationToken);
            if (u == null) await currencyRepository.CreateAsync(new Currency { Name = c }, cancellationToken);
            users.Add(u);
         });

         old.Users.ToList().AddRange(users);
      }


      await currencyRepository.UpdateAsync(old, cancellationToken);

      return request.currency;
   }
}