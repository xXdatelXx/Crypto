using Crypto.Application.Model;
using MediatR;

namespace Crypto.Application.Logic.Commands;

public record UpdateUserCommand(UserDTO user) : IRequest<UserDTO>;