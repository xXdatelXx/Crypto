using Crypto.Application.Model;
using Telegram.Bot.Types;

namespace Crypto.Telegram.MessageResponseHandler.Realisations;

public sealed class UpdateCredentialsResponse(HttpClient http) : IMessageResponse {
   public async Task<string?> HandleResponseAsync(Update update, CancellationToken token) {
      string message = update.Message.Text;
      string command = message.Split(' ')[0];

      if (command != "/updatecredentials")
         return null;

      var arguments = message.Split(' ').Skip(1).ToArray();

      if (arguments.Length != 2)
         return "Invalid command format. Use: /login <ByBitKey> <ByBitSecret>";

      string byBitKey = arguments[0];
      string byBitSecret = arguments[1];
      string telegramId = update.Message.From.Id.ToString();

      UserUpdate userUpdate = new(http);
      UserDTO? user = await userUpdate.Get(telegramId, token);
      if (user == null)
         return "User not found. Please ensure you are registered.";

      user.ByBitApiKey = byBitKey;
      user.ByBitApiSicret = byBitSecret;

      return await userUpdate.Update(user, token);
   }
}