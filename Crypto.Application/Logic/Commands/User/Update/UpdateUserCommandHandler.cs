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
    public class UpdateUserCommandHandler(IUserRepository userRepository, ICurrencyRepository currencyRepository) : IRequestHandler<UpdateUserCommand, UserDTO>
    {
        public async Task<UserDTO> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            User old = await userRepository.GetAsync(request.user.Id, cancellationToken);

            old.Id = request.user.Id;
            old.TelegramId = request.user.TelegramId;
            old.ByBitApiKey = request.user.ByBitApiKey;
            old.ByBitApiSicret = request.user.ByBitApiSicret;

            List<Currency> currencies = new();
            if(request.user.Currencies is not null)
            {
                request.user.Currencies?.ToList().ForEach(async c =>
                {
                    var c2 = await currencyRepository.GetByNameAsync(c, cancellationToken);
                    if (c2 == null)
                    {
                        await currencyRepository.CreateAsync(new Currency { Name = c }, cancellationToken);
                    }
                    currencies.Add(c2);
                });

                old.Currencies.ToList().AddRange(currencies);
            }
          
            // Дописать на ремувед



            await userRepository.UpdateAsync(old, cancellationToken);

            return request.user;
        }
    }
}
