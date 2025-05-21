using MediatR;
using Newtonsoft.Json.Linq;

namespace Crypto.Application.Logic.Queries.GreedFear;

public sealed class GreedFearQueryHandler : IRequestHandler<GreedFearQuery, string> {
   public async Task<string> Handle(GreedFearQuery request, CancellationToken cancellationToken) {
      string apiKey = "6698c20e-70a1-4750-afd5-4f8f451d5adc"; // Environment.GetEnvironmentVariable("CoinMarketCap:ApiKey");
      string url = "https://pro-api.coinmarketcap.com/v3/fear-and-greed/latest";

      ////
      using HttpClient client = new();
      ////
      client.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", apiKey);

      HttpResponseMessage response = await client.GetAsync(url, cancellationToken);
      string json = await response.Content.ReadAsStringAsync(cancellationToken);

      return JObject.Parse(json)["data"]?["value"].Value<string>();
   }
}