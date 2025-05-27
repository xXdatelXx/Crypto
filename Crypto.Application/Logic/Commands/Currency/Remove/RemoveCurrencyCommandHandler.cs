using Crypto.Data.Interface;
using Crypto.Data.Models;
using MediatR;

namespace Crypto.Application.Logic.Commands;

public sealed class RemoveCurrencyCommandHandler(ICurrencyRepository repository) : IRequestHandler<RemoveCurrencyCommand, Unit> {
   public async Task<Unit> Handle(RemoveCurrencyCommand request, CancellationToken cancellationToken) {
      Currency currency = await repository.GetAsync(request.id, cancellationToken);
      await repository.DeleteAsync(currency, cancellationToken);

      return Unit.Value;
   }
}