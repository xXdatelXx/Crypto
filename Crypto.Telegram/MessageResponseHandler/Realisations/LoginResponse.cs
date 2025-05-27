using Telegram.Bot.Types;

namespace Crypto.Telegram.Realisations;

public sealed class LoginResponse(HttpClient http) : IMessageResponse {
   public async Task<string?> HandleResponseAsync(Update update, CancellationToken token) {
      string message = update.Message.Text;
      var command = message.Split(' ')[0];

      if (command != "/login")
         return null;

      var arguments = message.Split(' ').Skip(1).ToArray();

      if (arguments.Length != 2)
         return "Invalid command format. Use: /login <ByBitKey> <ByBitSecret>";

      string byBitKey = arguments[0];
      string byBitSecret = arguments[1];
      string telegramId = update.Message.From.Id.ToString();

      var url = $"api/UserCRUD/CreateUser?" +
                $"telegramId={telegramId}" +
                $"&bybitKey={byBitKey}" +
                $"&bybitSicret={byBitSecret}";

      var response = await http.PostAsync(url, null, token);
      response.EnsureSuccessStatusCode();

      return response.IsSuccessStatusCode
         ? "User created successfully: "
         : "Failed to create user. Please check your input and try again.";
   }
}