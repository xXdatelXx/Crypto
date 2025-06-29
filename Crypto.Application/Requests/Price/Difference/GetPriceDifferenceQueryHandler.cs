using Crypto.Queries.Model;
using Crypto.Queries.Queries.Price;
using Crypto.Queries.Queries.Price.Difference;
using MediatR;

namespace Crypto.Queries.Handlers.Price.Difference;

public class GetPriceDifferenceQueryHandler(IHttpClientFactory httpClientFactory) : IRequestHandler<GetPriceDifferenceQuery, DifferencePriceResponse>
{
    public async Task<DifferencePriceResponse> Handle(GetPriceDifferenceQuery request, CancellationToken cancellationToken)
    {
        float old = await new GetPriceQueryHandler(httpClientFactory)
            .Handle(new GetPriceQuery(request.currency, request.time), cancellationToken);
        float current = await new GetPriceQueryHandler(httpClientFactory)
            .Handle(new GetPriceQuery(request.currency), cancellationToken);
        float difference = current - old;
        float percent = difference / old * 100;

        return new DifferencePriceResponse {
            Symbol = request.currency,
            Time = request.time,
            OldPrice = old,
            CurrentPrice = current,
            Difference = difference,
            PercentChange = Math.Round(percent, 2)
        };
    }
}
