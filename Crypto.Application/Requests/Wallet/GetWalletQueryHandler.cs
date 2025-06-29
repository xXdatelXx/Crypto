using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using Crypto.Data;
using Crypto.Queries.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace Crypto.Queries.Queries.Wallet;

// Writed by ChatGPT 
public sealed class GetWalletQueryHandler(CryptoDBContext dBContext, IHttpClientFactory httpClientFactory) : IRequestHandler<GetWalletQuery, WalletResponse> {
   public async Task<WalletResponse> Handle(GetWalletQuery request, CancellationToken cancellationToken) {
      var user = await dBContext.Users
         .Where(x => x.TelegramId == request.telegramId)
         .FirstOrDefaultAsync(cancellationToken);

      if (user == null)
         return null;

      var apiKey = user.ByBitApiKey;
      var apiSecret = user.ByBitApiSicret;

      var recvWindow = "10000";
      var query = "accountType=UNIFIED";

      using HttpClient client = httpClientFactory.CreateClient();

      var timeResponse = await client.GetFromJsonAsync<ServerTimeResponse>("https://api.bybit.com/v5/market/time", cancellationToken);
      var timestamp = timeResponse.time.ToString();

      var toSign = timestamp + apiKey + recvWindow + query;
      var signature = Sign(toSign, apiSecret);

      client.DefaultRequestHeaders.Add("X-BAPI-API-KEY", apiKey);
      client.DefaultRequestHeaders.Add("X-BAPI-TIMESTAMP", (string?)timestamp);
      client.DefaultRequestHeaders.Add("X-BAPI-RECV-WINDOW", recvWindow);
      client.DefaultRequestHeaders.Add("X-BAPI-SIGN", signature);
      client.DefaultRequestHeaders.Add("X-BAPI-SIGN-TYPE", "2");

      var url = $"https://api.bybit.com/v5/account/wallet-balance?{query}";
      var response = await client.GetStringAsync(url);

      var coins = JObject.Parse(response)?["result"]?["list"]?[0]?["coin"];

      var balances = coins?
         .Where(c => float.TryParse(c["walletBalance"]?.ToString(), out var b) && b > 0)
         .Select(c => (
            c["coin"].ToString(),
            float.Parse(c["walletBalance"]?.ToString())
         ))
         .ToList();

      return new WalletResponse {
         Assets = balances
      };

      static string Sign(string message, string secret) {
         var keyBytes = Encoding.UTF8.GetBytes(secret);
         var messageBytes = Encoding.UTF8.GetBytes(message);
         using var hmacsha256 = new HMACSHA256(keyBytes);
         var hash = hmacsha256.ComputeHash(messageBytes);
         return BitConverter.ToString(hash).Replace("-", "").ToLower();
      }
   }

   public sealed class ServerTimeResponse {
      public long time { get; set; }
   }
}