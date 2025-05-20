namespace Crypto.Telegram.Realisations;

public sealed class PriceHandler : IMessageHandler {
   public async Task<string> HandleMessageAsync(string message, CancellationToken token) {
      var command = message.Split(' ')[0];
      var arguments = message.Split(' ').Skip(1).ToArray();
      string currency = arguments[0].ToUpper();
      
      return command switch {
         "/price" => await http.GetFromJsonAsync<float>($"api/PriceEndPoint/GetPrice?currency={currency}&time={time}", token),
         "/difference" => "",
         _ => ""
      };
                
      var result = await http.GetFromJsonAsync<float>($"api/PriceEndPoint/GetPrice?currency={currency}&time={time}", token);
   }
}