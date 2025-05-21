using MediatR;

namespace Crypto.Application.Logic.Commands;

public record CreateCurrencyCommand(string Name) : IRequest<CurrencyDTO>;