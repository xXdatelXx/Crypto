using Crypto.Application.Model;
using MediatR;

namespace Crypto.Application.Logic.Commands.User.Update;

public record UpdateUserCommand(string telegramId, string byBitApiKey, string byBitApiSecret, IEnumerable<string> currencies) : IRequest<UserRequest>;