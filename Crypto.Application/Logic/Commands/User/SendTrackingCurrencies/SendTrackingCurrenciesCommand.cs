using MediatR;

namespace Crypto.Application.Logic.Commands.User.SendTrackingCurrencies;

public record SendTrackingCurrenciesCommand : IRequest<Unit>;