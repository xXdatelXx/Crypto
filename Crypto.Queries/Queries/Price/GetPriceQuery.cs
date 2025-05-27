using MediatR;

namespace Crypto.Application.Logic.Queries.Price;

public record GetPriceQuery(string currency, DateTime? time = null) : IRequest<float>;