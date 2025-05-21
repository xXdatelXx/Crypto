using System.Net.Http.Json;

namespace Crypto.Telegram.Realisations;

public sealed class LoginResponse(HttpClient http) : IMessageResponse {
    public async Task<string?> HandleResponseAsync(string message, CancellationToken token) {
        var command = message.Split(' ')[0];
        
        if(command != "/login")
            return null;
        
        var arguments = message.Split(' ').Skip(1).ToArray();

        if (arguments.Length != 2) {
            return "Usage: /login <ByBitKey> <ByBitSecret>";
        }

        string byBitKey = arguments[0];
        string byBitSecret = arguments[1];

        var result = await http.GetFromJsonAsync<string>(
            $"api/UserCRUD/CreateUser?telegramId={message}&bybitKey={byBitKey}&bybitSicret={byBitSecret}", token);

        return result != null 
            ? "User successfully created and logged in!" 
            : "Failed to create user. Please try again.";
    }
}
