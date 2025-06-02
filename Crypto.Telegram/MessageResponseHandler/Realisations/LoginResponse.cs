using Telegram.Bot.Types;

namespace Crypto.Telegram.MessageResponseHandler.Realisations;

public sealed class LoginResponse(HttpClient http) : IMessageResponse {
   public async Task<string?> HandleResponseAsync(string chatId, string command, CancellationToken token, params string[] args) {
      if (command != "/login")
         return null;

      if (args.Length != 2)
         return "Invalid command format. Use: /login <ByBitKey> <ByBitSecret>";

      string byBitKey = args[0];
      string byBitSecret = args[1];

      var url = $"api/UserCRUD/CreateUser?" +
                $"telegramId={chatId}" +
                $"&bybitKey={byBitKey}" +
                $"&bybitSicret={byBitSecret}";

      var response = await http.PostAsync(url, null, token);
      response.EnsureSuccessStatusCode();

      return response.IsSuccessStatusCode
         ? "User created successfully: "
         : "Failed to create user. Please check your input and try again.";
   }
}