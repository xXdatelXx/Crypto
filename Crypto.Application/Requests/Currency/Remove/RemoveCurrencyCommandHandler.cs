using Crypto.Data.Interface;
using MediatR;

namespace Crypto.Application.Logic.Commands.Currency.Remove;

public sealed class RemoveCurrencyCommandHandler(ICurrencyRepository repository) : IRequestHandler<RemoveCurrencyCommand, Unit> {
   public async Task<Unit> Handle(RemoveCurrencyCommand request, CancellationToken cancellationToken) {
      Data.Models.Currency currency = await repository.GetByIdAsync(request.id, cancellationToken);
      await repository.DeleteByIdAsync(currency, cancellationToken);

      return Unit.Value;
   }
}