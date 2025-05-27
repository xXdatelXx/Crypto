using Crypto.Application.Model;
using MediatR;

namespace Crypto.Application.Logic.Commands.User.Update;

public record UpdateUserCommand(UserDTO user) : IRequest<UserDTO>;