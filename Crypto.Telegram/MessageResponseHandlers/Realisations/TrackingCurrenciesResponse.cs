using Telegram.Bot.Types;

namespace Crypto.Telegram.MessageResponseHandler.Realisations;

public sealed class TrackingCurrenciesResponse(HttpClient http) : IMessageResponse {
   public async Task<string?> HandleResponseAsync(string chatId, string command, CancellationToken token, params string[] args) {
      if (command != "/tracking")
         return null;
      
      var model = await new UserUpdate(http).Get(chatId, token);
      if (model is null)
         return "No user found. Please register first using /login command.";

      PriceResponse price = new (http);
      List<string> result = [];
      
      foreach (var currency in model.Currencies) {
         var priceResponse = await price.HandleResponseAsync(chatId, "/price", token, currency);
         if (priceResponse != null)
            result.Add($"{currency} - {priceResponse}");
      }

      return string.Join('\n', result);
   }
}