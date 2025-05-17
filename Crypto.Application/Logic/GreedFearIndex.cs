using Newtonsoft.Json.Linq;

namespace Crypto.Application.Logic;

public class GreedFearIndex {
   public async Task<int> GetIndex() {
      string apiKey = Environment.GetEnvironmentVariable("CoinMarketCap/ApiKey");
      string url = "https://pro-api.coinmarketcap.com/v3/fear-and-greed/latest";

      using HttpClient client = new();
      client.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", apiKey);

      HttpResponseMessage response = await client.GetAsync(url);
      string json = await response.Content.ReadAsStringAsync();

      return (int)JObject.Parse(json)["data"]?["value"].Value<int>();
   }
}