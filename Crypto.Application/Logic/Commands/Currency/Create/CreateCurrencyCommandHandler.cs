using Crypto.Data.Interface;
using Crypto.Data.Models;
using FluentValidation;
using MediatR;

namespace Crypto.Application.Logic.Commands;

public class CreateCurrencyCommandHandler(ICurrencyRepository repository)
   : IRequestHandler<CreateCurrencyCommand, CurrencyDTO> {
   public async Task<CurrencyDTO> Handle(CreateCurrencyCommand request, CancellationToken cancellationToken) {
      var validator = new CreateCurrencyCommandValidator();
      var validationResult = await validator.ValidateAsync(request, cancellationToken);
      if (!validationResult.IsValid)
         throw new ValidationException(validationResult.Errors);
      
      Currency currency = new() {
         Name = request.Name
      };

      if (await repository.CheckDoublingAsync(currency, cancellationToken) == false)
         await repository.CreateAsync(currency, cancellationToken);

      return new CurrencyDTO {
         Id = currency.Id,
         Name = currency.Name
      };
   }
}