using Crypto.Queries.Model;
using MediatR;

namespace Crypto.Queries.Queries.User;

public record GetUserByTelegramIdQuery(string telegramId) : IRequest<UserResponse>;