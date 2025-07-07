using Crypto.Telegram.MessageResponseHandler;
using Telegram.Bot;

namespace Crypto.Telegram;

public sealed class Bot(ITelegramBotClient client, IHttpClientFactory httpClientFactory, IMessageResponseHandlerFactory messageFactory) : IBot {
   private readonly IMessageResponse _responses = messageFactory.Create(httpClientFactory.CreateClient());
   
   public async Task SendMessageAsync(string chatId, string command, CancellationToken token = default, params string[] args) {
      string response = await _responses.HandleResponseAsync(chatId, command, token, args);
      await client.SendMessage(chatId, response, cancellationToken: token);
   }
}