using Crypto.Data.Interface;
using Crypto.Queries.Model;
using MediatR;

namespace Crypto.Queries.Queries.User;

public sealed class GetUserQueryHandler(IUserRepository repository) : IRequestHandler<GetUserByTGIdQuery, UserResponse> {
   public async Task<UserResponse> Handle(GetUserByTGIdQuery request, CancellationToken cancellationToken) {
      Data.Models.User user = await repository.GetByTGIdAsync(request.telegramId, cancellationToken);
      return new UserResponse {
         Id = user.Id,
         TelegramId = user.TelegramId,
         ByBitApiKey = user.ByBitApiKey,
         ByBitApiSecret = user.ByBitApiSicret,
         Currencies = user.Currencies?.Select(c => c.Name).ToList() ?? []
      };
   }
}