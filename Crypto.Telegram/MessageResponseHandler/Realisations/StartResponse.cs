using Telegram.Bot.Types;

namespace Crypto.Telegram.Realisations;

public sealed class StartResponse : IMessageResponse {
   public async Task<string?> HandleResponseAsync(Update update, CancellationToken token) {
      string message = update.Message.Text;

      return await Task.FromResult<string?>(
         message == "/start"
            ? "Welcome to CryptoBot. Use /login to connect your account."
            : string.Empty
      );
   }
}