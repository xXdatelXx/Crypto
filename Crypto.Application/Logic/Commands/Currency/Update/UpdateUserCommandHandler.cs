using Crypto.Data.Interface;
using Crypto.Data.Models;
using FluentValidation;
using MediatR;

namespace Crypto.Application.Logic.Commands;

public class UpdateCurrencyCommandHandler(ICurrencyRepository currencyRepository, IUserRepository userRepository) : IRequestHandler<UpdateCurrencyCommand, CurrencyDTO> {
   public async Task<CurrencyDTO> Handle(UpdateCurrencyCommand request, CancellationToken cancellationToken) {
      var validator = new UpdateCurrencyCommandValidator();
      var validationResult = await validator.ValidateAsync(request, cancellationToken);
      if (!validationResult.IsValid)
         throw new ValidationException(validationResult.Errors);

      Currency old = await currencyRepository.GetAsync(request.currency.Id, cancellationToken);

      old.Name = request.currency.Name;

      List<User> users = [];
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