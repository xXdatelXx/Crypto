using Crypto.Telegram.Realisations;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;

namespace Crypto.Telegram;

public sealed class Bot(ITelegramBotClient client, IHttpClientFactory httpClientFactory, ILogger<Bot> logger) : BackgroundService {
   protected override async Task ExecuteAsync(CancellationToken cancellationToken) {
      client.StartReceiving(
         HandleUpdateAsync,
         HandleErrorAsync,
         cancellationToken: cancellationToken
      );

      var me = await client.GetMe(cancellationToken);
      logger.LogInformation($"Bot started: {me.Username}");
   }

   private async Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken token) {
      if (update.Message?.Text == null)
         return;

      using var http = httpClientFactory.CreateClient();
      http.BaseAddress = new Uri("https://localhost:44396/");

      string? response = await new MessageResponseHandler(
            new StartResponse(),
            new LoginResponse(http),
            new PriceResponse(http),
            new GreedFearResponse(http),
            new DifferenceResponse(http),
            new WalletResponse(http),
            new UpdateCredentialsResponse(http),
            new AddCurrencyResponse(http),
            new RemoveCurrencyResponse(http))
         .HandleResponseAsync(update, token);

      await client.SendMessage(update.Message.Chat.Id, response, cancellationToken: token);
   }

   private Task HandleErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken token) {
      logger.LogError(exception switch {
         ApiRequestException apiEx => $"Telegram API Error: [{apiEx.ErrorCode}] {apiEx.Message}",
         _ => exception.ToString()
      });

      return Task.CompletedTask;
   }
}