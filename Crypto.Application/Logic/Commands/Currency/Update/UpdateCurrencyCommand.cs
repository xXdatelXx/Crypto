using MediatR;

namespace Crypto.Application.Logic.Commands;

public record UpdateCurrencyCommand(CurrencyDTO currency) : IRequest<CurrencyDTO>;