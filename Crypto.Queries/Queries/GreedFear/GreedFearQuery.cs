using MediatR;

namespace Crypto.Application.Logic.Queries.GreedFear;

public record class GreedFearQuery : IRequest<string>;