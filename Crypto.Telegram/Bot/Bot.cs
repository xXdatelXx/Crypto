using Crypto.Telegram.MessageResponseHandler;
using Telegram.Bot;

namespace Crypto.Telegram;

public sealed class Bot(ITelegramBotClient client, IHttpClientFactory httpClientFactory, IMessageResponseHandlerFactory messageFactory) : IBot {
   public async Task SendMessageAsync(string chatId, string command, CancellationToken token = default, params string[] args) {
      using var http = httpClientFactory.CreateClient();
      string response = await messageFactory
         .Create(http)
         .HandleResponseAsync(chatId, command, token, args);

      await client.SendMessage(chatId, response, cancellationToken: token);
   }
}