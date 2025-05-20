using System.Net.Http.Json;
using System.Runtime.InteropServices.JavaScript;
using Crypto.Application.Model;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace Crypto.Telegram;

public class Bot(ITelegramBotClient client, IHttpClientFactory httpClientFactory, ILogger<Bot> logger) : BackgroundService 
{
    protected override async Task ExecuteAsync(CancellationToken cancellationToken) {
        client.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            cancellationToken: cancellationToken
        );
        
        var me = await client.GetMe(cancellationToken);
        logger.LogInformation($"Bot started: {me.Username}");
    }

    private async Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken token)
    {
        if (update.Message?.Text == null)
            return;

        var message = update.Message;
        var chatId = message.Chat.Id;
        var text = message.Text.ToLowerInvariant();
        var command = text.Split(' ')[0];
        var arguments = text.Split(' ').Skip(1).ToArray();
        
        // Write all arguments and indexes to the log
        for (int i = 0; i < arguments.Length; i++)
        {
            logger.LogInformation($"Argument {i}: {arguments[i]}");
        }
        
        var http = httpClientFactory.CreateClient();
        http.BaseAddress = new Uri("https://localhost:44396/");
        
        switch (command)
        {
            case "/start":
                await client.SendMessage(chatId, "Welcome to CryptoBot!", cancellationToken: token);
                break;
            case "/login":
                string byBitKey = arguments[0];
                string byBitSecret = arguments[1];

                var result2 =
                    await http.GetFromJsonAsync<UserDTO>($"api/UserCRUD/CreateUser?telegramId={update.Id}&bybitKey={byBitKey}&bybitSicret={byBitSecret}", token);
                
                await client.SendMessage(chatId, "User created", cancellationToken: token);
                break;
            case "/price": 
                string currency = arguments[0].ToUpper();
                string? time = arguments.Length > 1 ? arguments[1] : null;
                
                var result = await http.GetFromJsonAsync<float>($"api/PriceEndPoint/GetPrice?currency={currency}&time={time}", token);
                
                await client.SendMessage(chatId, $"Current price: {result}", cancellationToken: token);
                break;
            case "/greedfearindex":
                result = await http.GetFromJsonAsync<float>("api/GreedFearIndex/GetGreedFearIndex", token);
                await client.SendMessage(chatId, $"GreedFearIndex: {result}", cancellationToken: token);
                break;
            case "/AddCurrency":
                string currency2 = arguments[0].ToUpper();
                var result3 = await http.PostAsync($"api/CurrencyCRUD/CreateCurrency?name={currency2}", null, token);
                var response3 = result3.IsSuccessStatusCode
                    ? $"Currency '{currency2}' added successfully."
                    : $"Failed to add currency. Status: {result3.StatusCode}";
                //var cryptoApi = scope.ServiceProvider.GetRequiredService<ICryptoApiClient>();     
                //var result = await cryptoApi.AddCurrencyAsync(arguments[0], cancellationToken);
                //await botClient.SendMessage(chatId, result, cancellationToken: cancellationToken);
                break;
            }
        
        /*case "/AddCurrency":
            using (var scope = _serviceProvider.CreateScope())
            {
                //var cryptoApi = scope.ServiceProvider.GetRequiredService<ICryptoApiClient>();
                //var result = await cryptoApi.AddCurrencyAsync(arguments[0], cancellationToken);
                //await botClient.SendMessage(chatId, result, cancellationToken: cancellationToken);
            }
            break;
        case "/RemoveCurrency":
            using (var scope = _serviceProvider.CreateScope())
            {
                //var cryptoApi = scope.ServiceProvider.GetRequiredService<ICryptoApiClient>();
                //var result = await cryptoApi.RemoveCurrencyAsync(arguments[0], cancellationToken);
                //await botClient.SendMessage(chatId, result, cancellationToken: cancellationToken);
            }
            break;
        case "/Balance":
            break;*/
    }
    
    private Task HandleErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken token) {
        logger.LogError(exception switch
        {
            ApiRequestException apiEx => $"Telegram API Error: [{apiEx.ErrorCode}] {apiEx.Message}",
            _ => exception.ToString()
        });

        return Task.CompletedTask;
    }
}
   