using System.Net.Http.Json;
using Crypto.Application.Model;

namespace Crypto.Telegram.Realisations;

public sealed class LoginResponse(HttpClient http) : IMessageResponse {
   public async Task<string?> HandleResponseAsync(string message, CancellationToken token) {
      var command = message.Split(' ')[0];

      if (command != "/login")
         return null;

      var arguments = message.Split(' ').Skip(1).ToArray();

      if (arguments.Length != 3)
         return "Invalid command format. Use: /login <ByBitKey> <ByBitSecret> <TelegramID>";

      string byBitKey = arguments[0];
      string byBitSecret = arguments[1];
      string telegramId = arguments[2];
      
      var formData = new FormUrlEncodedContent(new[]
      {
         new KeyValuePair<string, string>("telegramId", telegramId),
         new KeyValuePair<string, string>("bybitKey", byBitKey),
         new KeyValuePair<string, string>("bybitSicret", byBitSecret) // typo kept to match your param name
      });

      
      var response = await http.PostAsync($"api/UserCRUD/CreateUser", formData, token);
      
      if (!response.IsSuccessStatusCode)
         return "Failed to create user. Please try again. 2";

      var result = await response.Content.ReadFromJsonAsync<UserDTO>(cancellationToken: token);

      return result != null
         ? "User successfully created and logged in!"
         : "Failed to create user. Please try again.";
   }
}