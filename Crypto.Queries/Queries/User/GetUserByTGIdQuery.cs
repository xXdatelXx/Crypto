using Crypto.Application.Model;
using Crypto.Queris.Model;
using MediatR;

namespace Crypto.Application.Logic.Queries;

public record GetUserByTGIdQuery(string telegramId) : IRequest<UserModel>;