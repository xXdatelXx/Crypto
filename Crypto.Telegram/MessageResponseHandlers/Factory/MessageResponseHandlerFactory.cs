using Crypto.Telegram.MessageResponseHandler.Realisations;

namespace Crypto.Telegram.MessageResponseHandler;

public sealed class MessageResponseHandlerFactory(string apiBaseAddress) : IMessageResponseHandlerFactory {
   public IMessageResponse Create(HttpClient http) {
      http.BaseAddress = new Uri(apiBaseAddress);

      return new MessageResponseHandler(
         new StartResponse(),
         new LoginResponse(http),
         new PriceResponse(http),
         new GreedFearResponse(http),
         new DifferenceResponse(http),
         new WalletResponse(http),
         new UpdateCredentialsResponse(http),
         new AddCurrencyResponse(http),
         new RemoveCurrencyResponse(http),
         new TrackingCurrenciesResponse(http));
   }
}