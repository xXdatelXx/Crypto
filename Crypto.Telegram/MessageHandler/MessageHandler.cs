namespace Crypto.Telegram;

public sealed class MessageHandler : IMessageHandler {
   private List<IMessageHandler> _handlers;
   
   public async Task<string> HandleMessageAsync(string message, CancellationToken token) {
     return  _handlers.Select(async void (h) => await h.HandleMessageAsync(message, token)).ToString();   
   }
}