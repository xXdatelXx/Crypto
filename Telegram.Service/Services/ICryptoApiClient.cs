namespace Telegram.Service.Services;

public interface ICryptoApiClient {
   Task<float> GetPriceAsync(string currency, string? time, CancellationToken token);
   /*Task<DifferenceDto> GetDifferenceAsync(string currency, DateTime time);
   Task<string> GetGreedFearIndexAsync();

   Task<UserDTO> CreateUserAsync(string telegramId, string key, string secret);
   Task<bool> UpdateUserAsync(UserDTO user);
   Task<bool> RemoveUserAsync(Guid userId);*/
}