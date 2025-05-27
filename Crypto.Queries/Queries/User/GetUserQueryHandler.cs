using Crypto.Data.Interface;
using Crypto.Queries.Model;
using MediatR;

namespace Crypto.Queries.Queries.User;

public sealed class GetUserQueryHandler(IUserRepository repository) : IRequestHandler<GetUserByTGIdQuery, UserModel> {
   public async Task<UserModel> Handle(GetUserByTGIdQuery request, CancellationToken cancellationToken) {
      Data.Models.User user = await repository.GetByTGIdAsync(request.telegramId, cancellationToken);
      return new UserModel {
         Id = user.Id,
         TelegramId = user.TelegramId,
         ByBitApiKey = user.ByBitApiKey,
         ByBitApiSicret = user.ByBitApiSicret,
         Currencies = user.Currencies?.Select(c => c.Name).ToList() ?? []
      };
   }
}