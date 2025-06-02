using Telegram.Bot.Types;

namespace Crypto.Telegram.MessageResponseHandler;

public interface IMessageResponse {
   Task<string?> HandleResponseAsync(string chatId, string command, CancellationToken token, params string[] args);
}