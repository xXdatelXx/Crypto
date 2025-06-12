using Crypto.Telegram.MessageResponseHandler.Realisations;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Telegram.Bot;

namespace Crypto.Telegram;

public sealed class Bot(ITelegramBotClient client, IHttpClientFactory httpClientFactory, IConfiguration configuration) : IBot {
   public async Task SendMessageAsync(string chatId, string command, CancellationToken token, params string[] args) {
      using var http = httpClientFactory.CreateClient();
      http.BaseAddress = new Uri("https://localhost:44396/");
      //http.BaseAddress = new Uri(configuration["ApiBaseAddress"]);

      string? response = await new MessageResponseHandler.MessageResponseHandler(
            new StartResponse(),
            new LoginResponse(http),
            new PriceResponse(http),
            new GreedFearResponse(http),
            new DifferenceResponse(http),
            new WalletResponse(http),
            new UpdateCredentialsResponse(http),
            new AddCurrencyResponse(http),
            new RemoveCurrencyResponse(http),
            new TrackingCurrenciesResponse(http))
         .HandleResponseAsync(chatId, command, token, args);

      await client.SendMessage(chatId, response ?? "Command not found", cancellationToken: token);
   }
}