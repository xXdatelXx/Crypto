using MediatR;

namespace Crypto.Application.Logic.Commands.User.Remove;

public record RemoveUserCommand(Guid id) : IRequest<Unit>;