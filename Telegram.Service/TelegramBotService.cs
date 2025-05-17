using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Service.Services;

namespace Telegram.Service;

public class TelegramBotService : BackgroundService
{
    private readonly ITelegramBotClient _botClient;
    private readonly ILogger<TelegramBotService> _logger;
    private readonly IServiceProvider _serviceProvider;

    public TelegramBotService(ITelegramBotClient botClient, ILogger<TelegramBotService> logger, IServiceProvider serviceProvider)
    {
        _botClient = botClient;
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _botClient.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            cancellationToken: stoppingToken
        );

        var me = await _botClient.GetMe(cancellationToken: stoppingToken);
        _logger.LogInformation($"Telegram Bot started. Username: {me.Username}");

        await Task.Delay(-1, stoppingToken);
    }

    private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update.Message?.Text == null) 
            return;

        var message = update.Message;
        var chatId = message.Chat.Id;
        var text = message.Text.ToLowerInvariant();
        var command = text.Split(' ')[0];
        var arguments = text.Split(' ').Skip(1).ToArray();

        switch (command) {
            case "/start":
                await botClient.SendMessage(chatId, "Welcome to CryptoBot!", cancellationToken: cancellationToken);
                break;
            case "/price": {
                using var scope = _serviceProvider.CreateScope();
                var cryptoApi = scope.ServiceProvider.GetRequiredService<ICryptoApiClient>();
                float price = await cryptoApi.GetPriceAsync(arguments[0], arguments[1], cancellationToken);
                await botClient.SendMessage(chatId, $"Current {arguments[0]} price: ${price}", cancellationToken: cancellationToken);
                break;
            }
        }
    }

    private Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken token)
    {
        var errorMessage = exception switch
        {
            ApiRequestException apiEx => $"Telegram API Error: [{apiEx.ErrorCode}] {apiEx.Message}",
            _ => exception.ToString()
        };

        _logger.LogError(errorMessage);
        return Task.CompletedTask;
    }
}