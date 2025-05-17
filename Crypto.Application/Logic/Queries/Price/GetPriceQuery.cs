using MediatR;

namespace Crypto.Application.Logic.Queries.Price;

public record class GetPriceQuery(string currency, DateTime? time = null) : IRequest<float>;