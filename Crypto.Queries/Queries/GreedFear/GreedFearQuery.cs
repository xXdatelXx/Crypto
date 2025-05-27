using MediatR;

namespace Crypto.Queries.Queries.GreedFear;

public record GreedFearQuery : IRequest<string>;