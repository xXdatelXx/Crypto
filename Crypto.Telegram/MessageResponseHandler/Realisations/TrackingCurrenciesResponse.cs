using Telegram.Bot.Types;

namespace Crypto.Telegram.MessageResponseHandler.Realisations;

public sealed class TrackingCurrenciesResponse(HttpClient http) : IMessageResponse {
   public async Task<string?> HandleResponseAsync(Update update, CancellationToken token) {
      string message = update.Message.Text;
      string command = message.Split(' ')[0];

      if (command != "/tracking")
         return null;
      
      string telegramId = update.Message.From.Id.ToString();
      var model = await new UserUpdate(http).Get(telegramId, token);
      if (model is null)
         return "No user found. Please register first using /login command.";

      PriceResponse price = new (http);
      List<string> result = [];
      
      foreach (var currency in model.Currencies) {
         var priceResponse = await price.HandleResponseAsync(new Update {
            Message = new Message {
               Text = "/price " + currency
            }
         }, token);
         if (priceResponse != null)
            result.Add($"{currency} - {priceResponse}");
      }

      return string.Join('\n', result);
   }
}