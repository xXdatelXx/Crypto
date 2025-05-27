using Crypto.Data.Interface;
using MediatR;

namespace Crypto.Application.Logic.Commands.Currency.Remove;

public sealed class RemoveCurrencyCommandHandler(ICurrencyRepository repository) : IRequestHandler<RemoveCurrencyCommand, Unit> {
   public async Task<Unit> Handle(RemoveCurrencyCommand request, CancellationToken cancellationToken) {
      Data.Models.Currency currency = await repository.GetAsync(request.id, cancellationToken);
      await repository.DeleteAsync(currency, cancellationToken);

      return Unit.Value;
   }
}