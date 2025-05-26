using System.Net.Http.Json;
using Crypto.Application.Model;
using Telegram.Bot.Types;

namespace Crypto.Telegram.Realisations;

public class UpdateUserResponse(HttpClient http) : IMessageResponse {
   public async Task<string?> HandleResponseAsync(Update update, CancellationToken token) {
      string message = update.Message.Text;
      var command = message.Split(' ')[0];

      if (command != "/updatecredentials")
         return null;

      var arguments = message.Split(' ').Skip(1).ToArray();

      if (arguments.Length != 2)
         return "Invalid command format. Use: /login <ByBitKey> <ByBitSecret>";

      string byBitKey = arguments[0];
      string byBitSecret = arguments[1];
      string telegramId = update.Message.From.Id.ToString();

      var getResponse = await http.GetAsync($"api/UserCRUD/GetUser?telegramId={telegramId}", token);
      if (!getResponse.IsSuccessStatusCode)
         return "Failed to retrieve user. Please ensure you are registered.";
      
      UserDTO? user = await getResponse.Content.ReadFromJsonAsync<UserDTO>(cancellationToken: token);
      if (user == null)
         return "User not found. Please ensure you are registered.";

      var url = $"api/UserCRUD/UpdateUser?" +
                $"id={user.Id}" +
                $"telegramId={telegramId}" +
                $"&bybitKey={byBitKey}" +
                $"&bybitSicret={byBitSecret}" + 
                $"&currencies={user.Currencies}";
      
      var updatePayload = new {
         id = user.Id,
         telegramId = telegramId,
         bybitKey = byBitKey,
         bybitSicret = byBitSecret,
         currencies = user.Currencies ?? new List<string>()
      };

      var jsonContent = JsonContent.Create(updatePayload);
      var updateResponse = await http.PostAsync("api/UserCRUD/UpdateUser", jsonContent, token);

      
      //var updateResponse = await http.PostAsync(url,null, token);
      
      if (!updateResponse.IsSuccessStatusCode)
         return "Failed to update user credentials. Please check your input and try again. " + updateResponse.ReasonPhrase;
      
      return "User credentials updated successfully.";
   }
}