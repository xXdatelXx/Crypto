using Telegram.Bot.Types;

namespace Crypto.Telegram.MessageResponseHandler.Realisations;

public sealed class StartResponse : IMessageResponse {
   public async Task<string?> HandleResponseAsync(string chatId, string command, CancellationToken token, params string[] args) {
      return await Task.FromResult<string?>(
         command == "/start"
            ? "Welcome to CryptoBot. Use /login to connect your account."
            : string.Empty
      );
   }
}