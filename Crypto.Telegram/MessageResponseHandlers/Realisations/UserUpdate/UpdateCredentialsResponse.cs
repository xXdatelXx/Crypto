using Crypto.Queries.Model;
using Telegram.Bot.Types;

namespace Crypto.Telegram.MessageResponseHandler.Realisations;

public sealed class UpdateCredentialsResponse(HttpClient http) : IMessageResponse {
   public async Task<string?> HandleResponseAsync(string chatId, string command, CancellationToken token, params string[] args) {
      if (command != "/updatecredentials")
         return null;

      if (args.Length != 2)
         return "Invalid command format. Use: /login <ByBitKey> <ByBitSecret>";

      string byBitKey = args[0];
      string byBitSecret = args[1];

      UserUpdate userUpdate = new(http);
      UserResponse? user = await userUpdate.Get(chatId, token);
      if (user == null)
         return "User not found. Please ensure you are registered.";

      var updatedUser = new UserResponse {
         Id = user.Id,
         TelegramId = user.TelegramId,
         ByBitApiKey = byBitKey,
         ByBitApiSecret = byBitSecret,
         Currencies = user.Currencies
      };

      return await userUpdate.Update(updatedUser, token);
   }
}