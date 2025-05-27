using Telegram.Bot.Types;

namespace Crypto.Telegram;

public interface IMessageResponse {
   Task<string?> HandleResponseAsync(Update update, CancellationToken token);
}