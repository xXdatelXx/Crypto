using Telegram.Bot.Types;

namespace Crypto.Telegram.MessageResponseHandler;

public sealed class MessageResponseHandler(params IMessageResponse[] handlers) : IMessageResponse {
   public async Task<string?> HandleResponseAsync(Update update, CancellationToken token) {
      string results = "";

      foreach (var h in handlers) {
         try {
            var result = await h.HandleResponseAsync(update, token);
            if (result != null) results += result + "\n";
         }
         catch (Exception e) {
            return $"An error occurred while processing the command: {e.Message}";
         }
      }

      return results;
   }
}