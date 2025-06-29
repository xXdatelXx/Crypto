using Crypto.Queries.Model;
using MediatR;

namespace Crypto.Queries.Queries.Price.Difference;

public record class GetPriceDifferenceQuery(string currency, DateTime time) : IRequest<DifferencePriceResponse>;