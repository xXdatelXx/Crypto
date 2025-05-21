namespace Crypto.Telegram.Realisations;

public sealed class StartResponse : IMessageResponse {
   public async Task<string?> HandleResponseAsync(string message, CancellationToken token) {
      return await Task.FromResult<string?>(
         message == "/start"
            ? "Welcome to CryptoBot! Use /login to connect your account."
            : string.Empty
      );
   }
}