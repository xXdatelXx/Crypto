using MediatR;

namespace Crypto.Application.Logic.Commands.Currency.Remove;

public record RemoveCurrencyCommand(Guid id) : IRequest<bool>;