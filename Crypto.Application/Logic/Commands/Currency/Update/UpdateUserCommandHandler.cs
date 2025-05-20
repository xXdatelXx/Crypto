using Crypto.Data.Interface;
using Crypto.Data.Models;
using MediatR;

namespace Crypto.Application.Logic.Commands
{
    public class UpdateCurrencyCommandHandler(ICurrencyRepository repository) : IRequestHandler<UpdateCurrencyCommand, CurrencyDTO>
    {
        public async Task<CurrencyDTO> Handle(UpdateCurrencyCommand request, CancellationToken cancellationToken)
        {
            Currency old = await repository.GetAsync(request.currency.Id, cancellationToken);

            old.Id = request.currency.Id;
            old.Name = request.currency.Name;

            await repository.UpdateAsync(old, cancellationToken);

            return request.currency;
        }
    }
}
