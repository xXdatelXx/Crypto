using Crypto.Queries.Model;
using Telegram.Bot.Types;

namespace Crypto.Telegram.MessageResponseHandler.Realisations;

public sealed class AddCurrencyResponse(HttpClient http) : IMessageResponse {
   public async Task<string?> HandleResponseAsync(string chatId, string command, CancellationToken token, params string[] args) {
      if (command != "/addcurrency")
         return null;

      if (args.Length != 1)
         return "Invalid command format. Use: /addcurrency <CurrencyName>";

      string currencyName = args[0];

      UserUpdate userUpdate = new(http);
      UserResponse? user = await userUpdate.Get(chatId, token);

      if (user == null)
         return "User not found. Please ensure you are registered.";

      user.Currencies = user.Currencies.Append(currencyName);

      return await userUpdate.Update(user, token);
   }
}