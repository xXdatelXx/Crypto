using MediatR;
using Newtonsoft.Json.Linq;

namespace Crypto.Queries.Queries.Price;

public sealed class GetPriceQueryHandler(IHttpClientFactory httpClientFactory) : IRequestHandler<GetPriceQuery, float> {
   public async Task<float> Handle(GetPriceQuery request, CancellationToken cancellationToken) {
      if (request.time > DateTime.UtcNow)
         throw new Exception("Time cannot be in the future");

      long timeMs = new DateTimeOffset(request.time ?? DateTime.UtcNow).ToUnixTimeMilliseconds();
      string url = request.time.HasValue
         ? $"https://api.bybit.com/v5/market/kline?category=linear&symbol={request.currency}&interval=1&start={timeMs}&limit=1"
         : $"https://api.bybit.com/v5/market/tickers?category=linear&symbol={request.currency}";

      using var client = httpClientFactory.CreateClient();
      HttpResponseMessage response = await client.GetAsync(url, cancellationToken);
      string json = await response.Content.ReadAsStringAsync(cancellationToken);
      try {
         JToken price = JObject.Parse(json)["result"]?["list"]?[0]?[request.time.HasValue ? 4 : "lastPrice"]?.ToString();
         return price.Value<float>();
      }
      catch (Exception) {
         return 0;
      }
   }
}