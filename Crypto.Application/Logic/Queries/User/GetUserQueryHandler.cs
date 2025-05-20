using Crypto.Application.Logic.Commands;
using Crypto.Application.Model;
using Crypto.Data.Interface;
using Crypto.Data.Models;
using MediatR;

namespace Crypto.Application.Logic.Queries;

public sealed class GetUserQueryHandler(IUserRepository repository) : IRequestHandler<GetUserQuery, UserDTO> 
{
   public async Task<UserDTO> Handle(GetUserQuery request, CancellationToken cancellationToken) {
      User user = await repository.GetAsync(request., cancellationToken);
      return new UserDTO() {
         Id = user.Id,
         TelegramId = user.TelegramId,
         ByBitApiKey = user.ByBitApiKey,
         ByBitApiSicret = user.ByBitApiSicret
      };
   }
}