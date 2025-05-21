using System.Net.Http.Json;

namespace Crypto.Telegram.Realisations;

public sealed class PriceResponse(HttpClient http) : IMessageResponse {
   public async Task<string?> HandleResponseAsync(string message, CancellationToken token) {
       var command = message.Split(' ')[0];
       var arguments = message.Split(' ').Skip(1).ToArray();
       string currency = arguments[0].ToUpper();
       string? time = arguments.Length > 1 ? arguments[1] : null;

       return command switch {
          "/price" => await http.GetFromJsonAsync<string>($"api/PriceEndPoint/GetPrice?currency={currency}&time={time}", token),
          "/difference" => "",
          _ => ""
       };
   }
}