using Crypto.Data.Interface;
using MediatR;

namespace Crypto.Application.Logic.Commands.Currency.Remove;

public sealed class RemoveCurrencyCommandHandler(ICurrencyRepository repository) : IRequestHandler<RemoveCurrencyCommand, bool> {
   public async Task<bool> Handle(RemoveCurrencyCommand request, CancellationToken cancellationToken) {
      Data.Models.Currency currency = await repository.GetByIdAsync(request.id, cancellationToken);
      return currency != null && await repository.DeleteByIdAsync(currency.Id, cancellationToken);;
   }
}