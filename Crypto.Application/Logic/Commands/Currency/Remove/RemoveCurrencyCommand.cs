using MediatR;

namespace Crypto.Application.Logic.Commands
{
    public record RemoveCurrencyCommand(Guid id) : IRequest<Unit>;
}
