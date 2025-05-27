using MediatR;

namespace Crypto.Application.Logic.Queries.GreedFear;

public record GreedFearQuery : IRequest<string>;