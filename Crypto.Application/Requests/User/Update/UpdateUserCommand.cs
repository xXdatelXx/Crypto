using Crypto.Queries.Model;
using MediatR;

namespace Crypto.Application.Logic.Commands.User.Update;

public record UpdateUserCommand(Guid id, string telegramId, string byBitApiKey, string byBitApiSecret, IEnumerable<string> currencies) : IRequest<UserResponse>;