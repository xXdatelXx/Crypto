using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;

namespace Crypto.Telegram;

public sealed class BotService(IBot bot, ITelegramBotClient client, ILogger<BotService> logger) : BackgroundService {
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
      string message = update.Message.Text;
      string command = message.Split(' ')[0];
      string[] arguments = message.Split(' ').Skip(1).ToArray();
      var telegramId = update.Message.From?.Id.ToString();
      
      await bot.SendMessageAsync(telegramId, command, token, arguments);
   }

   private Task HandleErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken token) {
      logger.LogError(exception switch {
         ApiRequestException apiEx => $"Telegram API Error: [{apiEx.ErrorCode}] {apiEx.Message}",
         _ => exception.ToString()
      });

      return Task.CompletedTask;
   }
}