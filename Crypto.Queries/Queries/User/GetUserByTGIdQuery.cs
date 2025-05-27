using Crypto.Queries.Model;
using MediatR;

namespace Crypto.Queries.Queries.User;

public record GetUserByTGIdQuery(string telegramId) : IRequest<UserModel>;