using System.Net.Http.Json;
using Telegram.Bot.Types;

namespace Crypto.Telegram.Realisations;

public sealed class GreedFearResponse(HttpClient http) : IMessageResponse {
   public async Task<string?> HandleResponseAsync(Update update, CancellationToken token) {
      string message = update.Message.Text;
      
      return message == "/greedfear"
         ? (await http.GetFromJsonAsync<int>("api/GreedFearIndex/GetGreedFearIndex", token)).ToString()
         : string.Empty;
   }
}