using Crypto.Application.Logic.Commands.User.Create;
using Crypto.Queries.Model;

namespace Crypto.Application.Requests.User.Extensions;

public static class UserMapper {
   public static Data.Models.User MatToUser(this CreateUserCommand request) {
      return new() {
         Id = Guid.NewGuid(),
         TelegramId = request.telegramId,
         ByBitApiKey = request.bybitKey,
         ByBitApiSicret = request.bybitSecret
      };
   }

   public static UserResponse MapToResponse(this Data.Models.User model) {
      return new UserResponse{
         Id = model.Id,
         TelegramId = model.TelegramId,
         ByBitApiKey = model.ByBitApiKey,
         ByBitApiSecret = model.ByBitApiSicret,
         Currencies = model.Currencies?.Select(c => c.Name).ToList() ?? []
      };  
   }
}