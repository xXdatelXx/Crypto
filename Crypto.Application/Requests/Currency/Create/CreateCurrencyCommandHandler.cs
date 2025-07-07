using Crypto.Application.Requests.Currency;
using Crypto.Application.Requests.Currency._Extensions;
using Crypto.Data.Interface;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Crypto.Application.Logic.Commands.Currency.Create;

public sealed class CreateCurrencyCommandHandler(ICurrencyRepository repository) : IRequestHandler<CreateCurrencyCommand, Guid> {
   public async Task<Guid> Handle(CreateCurrencyCommand request, CancellationToken cancellationToken) {
      Data.Models.Currency currency = new() {
         Id = Guid.NewGuid(),
         Name = request.Name
      };

      if (await repository.CheckDoublingAsync(currency, cancellationToken))
         throw new DbUpdateException("Currency is already exists");

      await repository.CreateAsync(currency, cancellationToken);

      return currency.Id;
   }
}