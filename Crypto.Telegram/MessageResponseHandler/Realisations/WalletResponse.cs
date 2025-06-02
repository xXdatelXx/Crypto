using System.Net.Http.Json;
using Crypto.Queries.Model;
using Telegram.Bot.Types;

namespace Crypto.Telegram.MessageResponseHandler.Realisations;

public class WalletResponse(HttpClient http) : IMessageResponse {
   public async Task<string?> HandleResponseAsync(string chatId, string command, CancellationToken token, params string[] args) {
      if (command != "/wallet")
         return null;

      var model = await http.GetFromJsonAsync<WalletModel>($"api/Wallet/GetWallet?telegramId={chatId}", token);

      return
         model is not null
            ? $"Total: {model.Total}\n USD" +
              "Assets:\n" +
              string.Join("\n", model.Assets.Select(a => $" - {a.Item1}: {a.Item2} USD"))
            : "No wallet found for this user.";
   }
}