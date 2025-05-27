using Crypto.Application.Model;
using MediatR;

namespace Crypto.Application.Logic.Commands.Currency.Update;

public record UpdateCurrencyCommand(CurrencyDTO currency) : IRequest<CurrencyDTO>;