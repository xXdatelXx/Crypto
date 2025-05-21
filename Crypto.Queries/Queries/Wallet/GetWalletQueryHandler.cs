using System.Security.Cryptography;
using System.Text;
using Crypto.Application.Model;
using Crypto.Data;
using Crypto.Data.Interface;
using Crypto.Queris.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace Crypto.Application.Logic.Queries.Wallet;

public class GetWalletQueryHandler(CryptoDBContext dBContext)  : IRequestHandler<GetWalletQuery, WalletModel>
{
   public async Task<WalletModel> Handle(GetWalletQuery request, CancellationToken cancellationToken)
   {
      var user = await dBContext.Users.Where(x => x.TelegramId == request.telegramId).Select(u => new UserModel()
      {
          ByBitApiKey = u.ByBitApiKey,
          ByBitApiSicret = u.ByBitApiSicret
      }).FirstOrDefaultAsync(cancellationToken);
      if(user == null)
      {
          return null;
      }

      string apiKey = user.ByBitApiKey;
      string apiSecret = user.ByBitApiSicret;

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
           .Select(c =>(
              c["coin"]?.ToString(),
              float.Parse(c["walletBalance"]?.ToString())
           ))
         .ToList();

        return new WalletModel
        {
            Assets = balances
        };

      static string Sign(string message, string secret) {
         using HMACSHA256 hmac = new(Encoding.UTF8.GetBytes(secret));
         byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
         return BitConverter.ToString(hash).Replace("-", "").ToLower();
      }
   }
}