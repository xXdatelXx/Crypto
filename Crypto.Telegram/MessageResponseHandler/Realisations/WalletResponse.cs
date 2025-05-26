using System.Net.Http.Json;
using Crypto.Application.Model;
using Telegram.Bot.Types;

namespace Crypto.Telegram.Realisations;

public class WalletResponse(HttpClient http) : IMessageResponse {
   public async Task<string?> HandleResponseAsync(Update update, CancellationToken token) {
      string message = update.Message.Text;
      string command = message.Split(' ')[0];

      if (command != "/wallet")
         return null;

      var telegramId = update.Message.From?.Id;
      var model = await http.GetFromJsonAsync<WalletModel>($"api/Wallet/GetWallet?telegramId={telegramId}", token);

      return   
         model is not null 
            ? $"Total: {model.Total}\n USD" +
              "Assets:\n" + 
              string.Join("\n", model.Assets.Select(a => $" - {a.Item1}: {a.Item2} USD")) 
            : "No wallet found for this user.";
   }
}