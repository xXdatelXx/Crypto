using Crypto.Application.Model;
using Crypto.Data.Interface;
using Crypto.Data.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Application.Logic.Commands
{
    public class UpdateUserCommandHandler(IUserRepository repository) : IRequestHandler<UpdateUserCommand, UserDTO>
    {
        public async Task<UserDTO> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            User old = await repository.GetAsync(request.user.Id, cancellationToken);

            old.Id = request.user.Id;
            old.TelegramId = request.user.TelegramId;
            old.ByBitApiKey = request.user.ByBitApiKey;
            old.ByBitApiSicret = request.user.ByBitApiSicret;

            await repository.UpdateAsync(old, cancellationToken);

            return request.user;
        }
    }
}
