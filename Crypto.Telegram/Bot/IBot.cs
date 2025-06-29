namespace Crypto.Telegram;

public interface IBot {
   Task SendMessageAsync(string chatId, string command, CancellationToken token = default, params string[] args);
}