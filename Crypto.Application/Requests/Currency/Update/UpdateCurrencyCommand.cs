using Crypto.Application.Requests.Currency;
using Crypto.Queries.Model;
using MediatR;

namespace Crypto.Application.Logic.Commands.Currency.Update;

public record UpdateCurrencyCommand(Guid id, string name, IEnumerable<Guid> users) : IRequest<CurrencyResponse>;