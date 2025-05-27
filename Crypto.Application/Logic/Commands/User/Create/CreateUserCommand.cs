using Crypto.Application.Model;
using MediatR;

namespace Crypto.Application.Logic.Commands.User.Create;

public record CreateUserCommand(string telegramId, string bybitKey, string bybitSicret) : IRequest<UserDTO>;