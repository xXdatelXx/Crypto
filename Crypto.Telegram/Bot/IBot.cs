namespace Crypto.Telegram;

public interface IBot {
   void SendMessage(string chatId, string command, params string[] args);
   Task SendMessageAsync(string chatId, string command, CancellationToken token, params string[] args);
}