using System.Net.Http.Json;

namespace Crypto.Telegram.Realisations;

public sealed class GreedFearResponse(HttpClient http) : IMessageResponse {
   public async Task<string?> HandleResponseAsync(string message, CancellationToken token) {
      return message == "/greedfear"
         ? (await http.GetFromJsonAsync<int>("api/GreedFearIndex/GetGreedFearIndex", token)).ToString()
         : string.Empty;
   }
}