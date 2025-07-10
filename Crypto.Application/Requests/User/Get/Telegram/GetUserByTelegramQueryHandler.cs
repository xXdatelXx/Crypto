using Crypto.Application.Requests.User.Extensions;
using Crypto.Data.Interface;
using Crypto.Queries.Model;
using MediatR;

namespace Crypto.Queries.Queries.User;

public sealed class GetUserByTelegramQueryHandler(IUserRepository repository) : IRequestHandler<GetUserByTelegramIdQuery, UserResponse> {
   public async Task<UserResponse> Handle(GetUserByTelegramIdQuery request, CancellationToken cancellationToken) {
      Data.Models.User user = await repository.GetByTGIdAsync(request.telegramId, cancellationToken);
      return user.MapToResponse();
   }
}