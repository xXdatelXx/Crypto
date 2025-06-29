using System.Net.Http.Json;
using Telegram.Bot.Types;

namespace Crypto.Telegram.MessageResponseHandler.Realisations;

public sealed class GreedFearResponse(HttpClient http) : IMessageResponse {
   public async Task<string?> HandleResponseAsync(string chatId, string command, CancellationToken token, params string[] args) {
      return command == "/greedfear"
         ? (await http.GetFromJsonAsync<int>("api/GreedFearIndex/GetGreedFearIndex", token)).ToString()
         : string.Empty;
   }
}