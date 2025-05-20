using Crypto.Application.Model;
using Crypto.Data.Interface;
using Crypto.Data.Models;
using MediatR;

namespace Crypto.Application.Logic.Commands
{
    public class CreateUserCommandHandler(IUserRepository repository) : IRequestHandler<CreateUserCommand, UserDTO>
    {
        public async Task<UserDTO> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            User user = new()
            {
                TelegramId = request.telegramId,
                ByBitApiKey = request.bybitKey,
                ByBitApiSicret = request.bybitSicret
            };

            //if (await repository.CheckDoublingAsync(user, cancellationToken) == false)
                await repository.Create(user, cancellationToken);

            return new UserDTO()
            {
                TelegramId = user.TelegramId,
                ByBitApiKey = user.ByBitApiKey,
                ByBitApiSicret = user.ByBitApiSicret
            };
        }
    }
}
