using Crypto.Application.Model;
using Crypto.Data.Interface;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Crypto.Application.Logic.Commands.Currency.Create;

public sealed class CreateCurrencyCommandHandler(ICurrencyRepository repository) : IRequestHandler<CreateCurrencyCommand, CurrencyRequest> {
   public async Task<CurrencyRequest> Handle(CreateCurrencyCommand request, CancellationToken cancellationToken) {
      var validator = new CreateCurrencyCommandValidator();
      var validationResult = await validator.ValidateAsync(request, cancellationToken);
      if (!validationResult.IsValid)
         throw new ValidationException(validationResult.Errors);

      Data.Models.Currency currency = new() {
         Name = request.Name
      };

      if (await repository.CheckDoublingAsync(currency, cancellationToken))
         throw new DbUpdateException("Currency is already exists");

      await repository.CreateAsync(currency, cancellationToken);

      return new CurrencyRequest {
         Id = currency.Id,
         Name = currency.Name
      };
   }
}