using MediatR;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Configuration;

namespace Crypto.Queries.Queries.GreedFear;

public sealed class GreedFearQueryHandler(IHttpClientFactory httpClientFactory, IConfiguration configuration) : IRequestHandler<GreedFearQuery, int> {
   public async Task<int> Handle(GreedFearQuery request, CancellationToken cancellationToken) {
      string? apiKey = configuration["CoinMarketCapApiKey"];
      
      if (string.IsNullOrEmpty(apiKey)) {
         throw new InvalidOperationException("CoinMarketCap API key is not configured.");
      }
      
      string url = "https://pro-api.coinmarketcap.com/v3/fear-and-greed/latest";

      using HttpClient client = httpClientFactory.CreateClient();
      client.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", apiKey);

      HttpResponseMessage response = await client.GetAsync(url, cancellationToken);
      string json = await response.Content.ReadAsStringAsync(cancellationToken);

      return JObject.Parse(json)["data"]?["value"].Value<int>() ?? 0;
   }
}
