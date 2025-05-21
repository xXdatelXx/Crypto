using Crypto.Data.Interface;
using Crypto.Data.Models;
using Crypto.Queris.Model;
using MediatR;

namespace Crypto.Application.Logic.Queries;

public sealed class GetUserQueryHandler(IUserRepository repository) : IRequestHandler<GetUserByTGIdQuery, UserModel> 
{
   public async Task<UserModel> Handle(GetUserByTGIdQuery request, CancellationToken cancellationToken) {
      User user = await repository.GetByTGIdAsync(request.telegramId, cancellationToken);
      return new UserModel {
         Id = user.Id,
         TelegramId = user.TelegramId,
         ByBitApiKey = user.ByBitApiKey,
         ByBitApiSicret = user.ByBitApiSicret
      };
   }
}