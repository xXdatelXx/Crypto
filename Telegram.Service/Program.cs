using Telegram.Bot;
using Telegram.Service;
using Telegram.Service.Services;

var builder = Host.CreateDefaultBuilder(args)
   .ConfigureServices((hostContext, services) =>
   {
      services.AddSingleton<ITelegramBotClient>(provider =>
            new TelegramBotClient("8040659146:AAEeJELy6WOw9PJPiEi-PIXdJpOKHzzNOVw")
      );

      services.AddHttpClient<ICryptoApiClient, CryptoApiClient>(client => 
         client.BaseAddress = new Uri("http://localhost:5432/"));

      services.AddHostedService<TelegramBotService>();
   });

await builder.RunConsoleAsync();