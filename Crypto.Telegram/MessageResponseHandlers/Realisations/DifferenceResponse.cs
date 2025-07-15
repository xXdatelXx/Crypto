using System.Net.Http.Json;
using Crypto.Queries.Model;
using Telegram.Bot.Types;

namespace Crypto.Telegram.MessageResponseHandler.Realisations;

public sealed class DifferenceResponse(HttpClient http) : IMessageResponse {
   public async Task<string?> HandleResponseAsync(string chatId, string command, CancellationToken token, params string[] args) {
      if (command != "/difference")
         return null;

      if (args.Length > 2)
         return "Invalid command format. Use: /difference <currency> <time>";

      string currency = args[0].ToUpper();
      string? time = args.Length > 1 ? args[1] : null;

      var model = await http.GetFromJsonAsync<DifferencePriceResponse>($"api/PriceEndPoint/GetDifference?currency={currency}&time={time}", token);

      return $"Symbol: {model.Symbol}\n" +
             $"Time: {model.Time}\n" +
             $"Old Price: {model.OldPrice}\n" +
             $"Current Price: {model.CurrentPrice}\n" +
             $"Difference: {model.Difference}\n" +
             $"Percent Change: {model.PercentChange}";
   }
}