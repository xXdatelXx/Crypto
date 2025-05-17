using System.Net.Http.Json;
using Telegram.Service.Services;

public class CryptoApiClient(HttpClient http) : ICryptoApiClient {
   public async Task<float> GetPriceAsync(string currency, string? time, CancellationToken token) {
      return await http.GetFromJsonAsync<float>($"api/price/GetPrice?currency={currency}");
   }
}