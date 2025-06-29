using MediatR;

namespace Crypto.Queries.Queries.Price;

public record GetPriceQuery(string currency, DateTime? time = null) : IRequest<float>;