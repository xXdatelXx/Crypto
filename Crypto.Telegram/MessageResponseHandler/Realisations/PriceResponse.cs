using System.Net.Http.Json;

namespace Crypto.Telegram.MessageResponseHandler.Realisations;

public sealed class PriceResponse(HttpClient http) : IMessageResponse {
   public async Task<string?> HandleResponseAsync(string chatId, string command, CancellationToken token, params string[] args) {
      if (command != "/price")
         return null;

      if (args.Length > 2)
         return "Invalid command format. Use: /price <currency> <time>";

      string currency = args[0].ToUpper();
      string? time = args.Length > 1 ? args[1] : null;

      return (await http.GetFromJsonAsync<float>($"api/PriceEndPoint/GetPrice?currency={currency}&time={time}", token)).ToString();
   }
}