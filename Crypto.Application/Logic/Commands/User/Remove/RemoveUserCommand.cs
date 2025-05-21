using MediatR;

namespace Crypto.Application.Logic.Commands;

public record RemoveUserCommand(Guid id) : IRequest<Unit>;