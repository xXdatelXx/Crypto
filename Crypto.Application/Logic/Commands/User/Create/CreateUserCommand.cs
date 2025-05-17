using Crypto.Application.Model;
using Crypto.Data.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Application.Logic.Commands
{
    public record CreateUserCommand(string telegramId, string bybitKey, string bybitSicret) : IRequest<UserDTO>;
}
