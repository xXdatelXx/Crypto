using Crypto.Telegram.MessageResponseHandler;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;

namespace Crypto.Telegram.Configuration;

public static class ServiceCollectionExtensions {
   public static IServiceCollection AddTelegram(this IServiceCollection services, string telegramBotToken, string apiBaseAddress)
   {
      services.AddSingleton<ITelegramBotClient>(new TelegramBotClient(
         telegramBotToken
      ));
      services.AddSingleton<IBot, Bot>();
      services.AddHostedService<BotService>();
      services.AddSingleton(new MessageResponseHandlerFactory(apiBaseAddress));
      
      return services;
   }
}