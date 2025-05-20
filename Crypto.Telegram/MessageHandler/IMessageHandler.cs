namespace Crypto.Telegram;

public interface IMessageHandler {
   Task<string> HandleMessageAsync(string message, CancellationToken token);
}