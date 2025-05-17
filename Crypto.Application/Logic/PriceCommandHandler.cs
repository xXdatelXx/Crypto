using Crypto.Application.Model;
using Crypto.Data.Interface;
using Crypto.Data.Models;
using Crypto.Data.Repository;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;

namespace Crypto.Application.Logic;

public class PriceCommandHandler
{
    private readonly ICurrencyRepository _repository;

    public async Task<float> GetPrice(string currency, DateTime? time = null)
    {
        if (time > DateTime.UtcNow)
            throw new Exception("Time cannot be in the future");
        if (!currency.EndsWith("USDT", StringComparison.OrdinalIgnoreCase))
            currency += "USDT";

        long timeMs = new DateTimeOffset(time ?? DateTime.UtcNow).ToUnixTimeMilliseconds();
        string url = time.HasValue
           ? $"https://api.bybit.com/v5/market/kline?category=linear&symbol={currency}&interval=1&start={timeMs}&limit=1"
           : $"https://api.bybit.com/v5/market/tickers?category=linear&symbol={currency}";
        //////
        var client = new HttpClient();
        ///////
        HttpResponseMessage response = await client.GetAsync(url);
        string json = await response.Content.ReadAsStringAsync();
        JToken price = JObject.Parse(json)["result"]?["list"]?[0]?[time.HasValue ? 4 : "lastPrice"]?.ToString();

        /*var test = new Currency
        {
            Id = new Guid(),
            Name = "Test",
        };
        await _repository.Create(test, new CancellationToken());*/
        return price.Value<float>();
    }

    public async Task<DifferenceDto> Difference(string currency, DateTime from)
    {
        float old = await GetPrice(currency, from);
        float current = await GetPrice(currency);
        float difference = current - old;
        float percent = difference / old * 100;

        return new DifferenceDto()
        {
            Symbol = currency,
            Time = from,
            OldPrice = old,
            CurrentPrice = current,
            Difference = difference,
            PercentChange = Math.Round(percent, 2)
        };
    }
}
