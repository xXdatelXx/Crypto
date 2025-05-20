using Crypto.Application.Model;
using MediatR;

namespace Crypto.Application.Logic.Queries;

public record GetUserQuery(string telegramId) : IRequest<UserDTO>;