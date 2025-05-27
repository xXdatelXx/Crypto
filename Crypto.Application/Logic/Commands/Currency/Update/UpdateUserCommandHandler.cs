using Crypto.Application.Model;
using Crypto.Data.Interface;
using FluentValidation;
using MediatR;

namespace Crypto.Application.Logic.Commands.Currency.Update;

public sealed class UpdateCurrencyCommandHandler(ICurrencyRepository currencyRepository, IUserRepository userRepository) : IRequestHandler<UpdateCurrencyCommand, CurrencyDTO> {
   public async Task<CurrencyDTO> Handle(UpdateCurrencyCommand request, CancellationToken cancellationToken) {
      var validator = new UpdateCurrencyCommandValidator();
      var validationResult = await validator.ValidateAsync(request, cancellationToken);
      if (!validationResult.IsValid)
         throw new ValidationException(validationResult.Errors);

      Data.Models.Currency old = await currencyRepository.GetAsync(request.currency.Id, cancellationToken);

      old.Name = request.currency.Name;

      List<Data.Models.User> users = [];
      request.currency.Users?.ToList().ForEach(async c => {
         var u = await userRepository.GetByTGIdAsync(c, cancellationToken);
         if (u == null) 
            await currencyRepository.CreateAsync(new Data.Models.Currency { Name = c }, cancellationToken);
         users.Add(u);
      });

      old.Users.ToList().AddRange(users);

      await currencyRepository.UpdateAsync(old, cancellationToken);

      return request.currency;
   }
}