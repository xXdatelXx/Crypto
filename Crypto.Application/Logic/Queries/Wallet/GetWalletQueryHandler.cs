using Crypto.Application.Logic.Queries.Price;
using Crypto.Application.Model;
using MediatR;
using Newtonsoft.Json.Linq;

namespace Crypto.Application.Logic.Queries.Wallet;

public class GetWalletQueryHandler  : IRequestHandler<GetWalletQuery, WalletDTO>
{
   public async Task<WalletDTO> Handle(GetWalletQuery request, CancellationToken cancellationToken)
   {
      return default;
      /*string apiKey = "HoV2kLSvzfFWpPXEyk";
      string apiSecret = "VBSAhnECjvLN5YAM84YCVd3xHISRHZDmzRPV";
      string recvWindow = "5000";
      string query = "accountType=UNIFIED";
      string timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();

      string toSign = timestamp + apiKey + recvWindow + query;
      string signature = Sign(toSign, apiSecret);

      using HttpClient client = new();
      client.DefaultRequestHeaders.Add("X-BAPI-API-KEY", apiKey);
      client.DefaultRequestHeaders.Add("X-BAPI-TIMESTAMP", timestamp);
      client.DefaultRequestHeaders.Add("X-BAPI-RECV-WINDOW", recvWindow);
      client.DefaultRequestHeaders.Add("X-BAPI-SIGN", signature);
      client.DefaultRequestHeaders.Add("X-BAPI-SIGN-TYPE", "2");

      string url = $"https://api.bybit.com/v5/account/wallet-balance?{query}";
      string response = await client.GetStringAsync(url);

      JToken? coins = JObject.Parse(response)["result"]?["list"]?[0]?["coin"];

      var balances = coins
         .Where(c => decimal.TryParse(c["walletBalance"]?.ToString(), out decimal b) && b > 0)
         .Select(c => new {
            Currency = c["coin"]?.ToString(),
            Balance = c["walletBalance"]?.ToString(),
            USDValue = decimal.TryParse(c["usdValue"]?.ToString(), out decimal usd) ? Math.Round(usd, 2) : 0
         })
         .ToList();

      decimal total = balances.Sum(x => x.USDValue);

      return Ok(new {
         Wallet = balances,
         TotalUSD = Math.Round(total, 2)
      });

      static string Sign(string message, string secret) {
         using HMACSHA256 hmac = new(Encoding.UTF8.GetBytes(secret));
         byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
         return BitConverter.ToString(hash).Replace("-", "").ToLower();
      }*/
   }
}