using Crypto.Application.Model;
using MediatR;

namespace Crypto.Application.Logic.Commands;

public record CreateUserCommand(string telegramId, string bybitKey, string bybitSicret) : IRequest<UserDTO>;