using Crypto.Application.Logic.Commands;
using Crypto.Application.Model;
using Crypto.Data.Interface;
using Crypto.Data.Models;
using MediatR;

namespace Crypto.Application.Logic.Queries;

public sealed class GetCurrencyQueryHandler(ICurrencyRepository repository) : IRequestHandler<GetCurrencyQuery, CurrencyDTO>
{
   public async Task<CurrencyDTO> Handle(GetCurrencyQuery request, CancellationToken cancellationToken) {
      Currency currency = await repository.GetAsync(request.currency, cancellationToken);
      return new CurrencyDTO() {
         Id = currency.Id,
         Name = currency.Name,
      };
   }
}
