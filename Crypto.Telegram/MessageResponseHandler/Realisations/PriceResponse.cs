using System.Net.Http.Json;

namespace Crypto.Telegram.Realisations;

public sealed class PriceResponse(HttpClient http) : IMessageResponse {
   public async Task<string?> HandleResponseAsync(string message, CancellationToken token) {
      string command = message.Split(' ')[0];
      
      if(command != "/price")
         return null;
      
      string[] arguments = message.Split(' ').Skip(1).ToArray();
      
      if (arguments.Length > 2)
         return "Invalid command format. Use: /price <currency> <time>";
      
      string currency = arguments[0].ToUpper();
      string? time = arguments.Length > 1 ? arguments[1] : null;

      return (await http.GetFromJsonAsync<float>($"api/PriceEndPoint/GetPrice?currency={currency}&time={time}", token)).ToString();
   }
}
