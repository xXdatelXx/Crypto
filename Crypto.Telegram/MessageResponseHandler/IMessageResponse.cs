namespace Crypto.Telegram;

public interface IMessageResponse {
   Task<string?> HandleResponseAsync(string message, CancellationToken token);
}