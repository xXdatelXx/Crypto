#region

using Crypto.Application.Model;
using Telegram.Bot.Types;

#endregion

namespace Crypto.Telegram.MessageResponseHandler.Realisations.UserUpdate;

public sealed class AddCurrencyResponse(HttpClient http) : IMessageResponse {
   public async Task<string?> HandleResponseAsync(Update update, CancellationToken token) {
      string message = update.Message.Text;
      string command = message.Split(' ')[0];

      if (command != "/addcurrency")
         return null;

      var arguments = message.Split(' ').Skip(1).ToArray();

      if (arguments.Length != 1)
         return "Invalid command format. Use: /addcurrency <CurrencyName>";

      string currencyName = arguments[0];
      string telegramId = update.Message.From.Id.ToString();

      UserUpdate userUpdate = new(http);
      UserDTO? user = await userUpdate.Get(telegramId, token);

      if (user == null)
         return "User not found. Please ensure you are registered.";

      user.Currencies = user.Currencies.Append(currencyName);

      return await userUpdate.Update(user, token);
   }
}