using Crypto.Data.Interface;
using Crypto.Queris.Model;
using MediatR;

namespace Crypto.Application.Logic.Queries;

public sealed class GetCurrencyQueryHandler(ICurrencyRepository repository)
   : IRequestHandler<GetCurrencyQuery, CurrencyModel> {
   public async Task<CurrencyModel> Handle(GetCurrencyQuery request, CancellationToken cancellationToken) {
      /*  Currency currency = await repository.GetAsync(request.currency, cancellationToken);
        return new CurrencyDTO() {
           Id = currency.Id,
           Name = currency.Name,
        };*/
      return null;
   }
}