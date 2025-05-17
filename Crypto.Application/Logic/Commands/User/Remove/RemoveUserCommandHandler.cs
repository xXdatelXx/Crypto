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
    public class RemoveUserCommandHandler(IUserRepository repository) : IRequestHandler<RemoveUserCommand, Unit>
    {
        public async Task<Unit> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
        {
            User user = await repository.GetAsync(request.id, cancellationToken);
            await repository.DeleteAsync(user, cancellationToken);

            return new Unit();
        }
    }
}