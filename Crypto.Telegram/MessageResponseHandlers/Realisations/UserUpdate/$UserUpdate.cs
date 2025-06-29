using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Crypto.Queries.Model;

namespace Crypto.Telegram.MessageResponseHandler.Realisations;

// Rewrite
public sealed class UserUpdate(HttpClient http) {
   public async Task<UserResponse?> Get(string telegramId, CancellationToken token) {
      var getResponse = await http.GetAsync($"api/UserCRUD/GetUser?telegramId={telegramId}", token);
      if (!getResponse.IsSuccessStatusCode)
         return null;

      return await getResponse.Content.ReadFromJsonAsync<UserResponse>(token);
   }

   public async Task<string> Update(UserResponse user, CancellationToken token) {
      var url = "/api/UserCRUD/UpdateUser" +
                $"?id={user.Id}" +
                $"&telegramId={user.TelegramId}" +
                $"&bybitKey={user.ByBitApiKey}" +
                $"&bybitSicret={user.ByBitApiSecret}";

      string jsonBody = JsonSerializer.Serialize(user.Currencies?.ToList() ?? []);
      var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

      var updateResponse = await http.PostAsync(url, content, token);
      return updateResponse.IsSuccessStatusCode
         ? "User updated successfully."
         : "Failed to update user. Please check your input and try again.";
   }
}