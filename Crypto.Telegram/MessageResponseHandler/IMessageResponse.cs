using Telegram.Bot.Types;

namespace Crypto.Telegram.MessageResponseHandler;

public interface IMessageResponse {
   Task<string?> HandleResponseAsync(Update update, CancellationToken token);
}