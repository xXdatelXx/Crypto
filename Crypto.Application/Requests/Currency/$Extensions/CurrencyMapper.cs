using Crypto.Application.Requests.User.Extensions;

namespace Crypto.Application.Requests.Currency._Extensions;

public static class CurrencyMapper {
   public static CurrencyResponse MapToResponse(this Data.Models.Currency currency) {
      return new CurrencyResponse() {
         Id = currency.Id,
         Name = currency.Name,
         Users = currency.Users!.Select(i => i.MapToResponse())
      };
   }
}