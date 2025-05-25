using System.Net.Http.Json;
using Crypto.Queris.Model;

namespace Crypto.Telegram.Realisations;

public sealed class DifferenceResponse(HttpClient http) : IMessageResponse {
   public async Task<string?> HandleResponseAsync(string message, CancellationToken token) {
      string command = message.Split(' ')[0];
      
      if(command != "/difference")
         return null;
      
      string[] arguments = message.Split(' ').Skip(1).ToArray();
      
      if (arguments.Length > 2)
         return "Invalid command format. Use: /difference <currency> <time>";
      
      string currency = arguments[0].ToUpper();
      string? time = arguments.Length > 1 ? arguments[1] : null;

      var model = await http.GetFromJsonAsync<DifferenceModel>($"api/PriceEndPoint/GetDifference?currency={currency}&time={time}", token);
      
      return $"Symbol: {model.Symbol}\n" +
             $"Time: {model.Time}\n" +
             $"Old Price: {model.OldPrice}\n" +
             $"Current Price: {model.CurrentPrice}\n" +
             $"Difference: {model.Difference}\n" +
             $"Percent Change: {model.PercentChange}";
   }
}
