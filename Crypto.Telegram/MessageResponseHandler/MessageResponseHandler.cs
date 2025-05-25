using Telegram.Bot.Types;

namespace Crypto.Telegram;

public sealed class MessageResponseHandler(params IMessageResponse[] handlers) : IMessageResponse {
   public async Task<string?> HandleResponseAsync(Update update, CancellationToken token) {
      string results = "";

      foreach (var h in handlers) {
         var result = await h.HandleResponseAsync(update, token);
         if (result != null) results += result + "\n";
      }

      return results;
   }
}