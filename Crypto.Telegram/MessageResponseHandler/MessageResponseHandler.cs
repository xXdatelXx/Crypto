namespace Crypto.Telegram;

public sealed class MessageResponseHandler(params IMessageResponse[] handlers) : IMessageResponse {
   public async Task<string?> HandleResponseAsync(string message, CancellationToken token) => 
      handlers.Select(async h => await h.HandleResponseAsync(message, token)).ToString();
}