namespace Crypto.Telegram;

public sealed class MessageResponseHandler(params IMessageResponse[] handlers) : IMessageResponse {
   public async Task<string?> HandleResponseAsync(string message, CancellationToken token) {
      string results = "";
      
      foreach (var result in (handlers.Select(async h => await h.HandleResponseAsync(message, token)))) {
         if (result.Result != null) 
            results += result.Result + "\n";
      }
      return results;
   }
}