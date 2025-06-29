namespace Crypto.Telegram.MessageResponseHandler;

public interface IMessageResponseHandlerFactory {
   IMessageResponse Create(HttpClient http);
}