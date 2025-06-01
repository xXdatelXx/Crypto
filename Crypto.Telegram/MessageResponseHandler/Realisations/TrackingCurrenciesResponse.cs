using Telegram.Bot.Types;

namespace Crypto.Telegram.MessageResponseHandler.Realisations;

public sealed class TrackingCurrenciesResponse(HttpClient http) : IMessageResponse {
   public async Task<string?> HandleResponseAsync(Update update, CancellationToken token) {
      string message = update.Message.Text;
      string command = message.Split(' ')[0];

      if (command != "/tracking")
         return null;
      
      string telegramId = update.Message.From.Id.ToString();
      var model = await new UserUpdate.UserUpdate(http).Get(telegramId, token);
      if (model is null)
         return "No user found. Please register first using /login command.";

      var currencies = model.Currencies;
      if (!currencies.Any())
         return "No currencies are being tracked. Use /addcurrency command to add currencies.";

      var price = new PriceResponse(http);
      var prices = await Task.WhenAll(currencies.Select(currency => price.HandleResponseAsync(new Update {
         Message = new Message {
            Text = $"/price {currency}"
         }
      }, token)));

      return string.Join("\n", currencies.Zip(prices, (n, p) => $"{n} - {p}"));
   }
}