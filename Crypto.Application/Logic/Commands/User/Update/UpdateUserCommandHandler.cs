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

            if (old.Currencies == null)
                old.Currencies = new List<Currency>();

            foreach (var c in request.user.Currencies.ToList()) {
                var c2 =  await currencyRepository.GetByNameAsync(c, cancellationToken);
                if (c2 == null) {
                    await currencyRepository.CreateAsync(new Currency { Name = c, Users = new List<User> { old }}, cancellationToken);
                    //old.Currencies.ToList().Add( currencyRepository.GetByNameAsync(c, cancellationToken).Result);
                }
                else {
                    if(c2.Users == null)    
                        c2.Users = new List<User> { old };
                    else 
                        c2.Users.Add(old);
                    currencyRepository.UpdateAsync(c2, cancellationToken);
                    
                    //old.Currencies.Remove(c2);
                    //if (!old.Currencies.Any(c => c.Name == c2.Name))
                    //     old.Currencies.Add(c2);
                }
            }
            
            var toRemove = old.Currencies?.Where(c => !request.user.Currencies.Contains(c.Name)).ToList();
            foreach (var currency in toRemove) {
                old.Currencies.Remove(currency);
                //currency.Users.Remove(old);
                //if (currency.Users.Count == 0) {
                //    await currencyRepository.DeleteAsync(currency, cancellationToken);
                //}
            }
            
            /*request.user.Currencies.ToList().ForEach( c =>
            {
                var c2 =  currencyRepository.GetByNameAsync(c, cancellationToken).Result;
                if (c2 == null) {
                    await currencyRepository.CreateAsync(new Currency { Name = c, Users = new List<User> { old }}, cancellationToken);
                    //old.Currencies.ToList().Add( currencyRepository.GetByNameAsync(c, cancellationToken).Result);
                }
                else {
                    if(c2.Users == null)    
                        c2.Users = new List<User> { old };
                    else 
                        c2.Users.Add(old);
                    currencyRepository.UpdateAsync(c2, cancellationToken);
                    
                    //old.Currencies.Remove(c2);
                    //if (!old.Currencies.Any(c => c.Name == c2.Name))
                    //     old.Currencies.Add(c2);
                }
            });*/

            // if(old.Currencies.ToList().Count > request.user.Currencies?.ToList().Count)
            // {
            //     var remove = old.Currencies.ToList().Where(c => !request.user.Currencies.Contains(c.Name)).ToList();
            //     remove.ForEach(c =>c.Removed = true);
            // }


             userRepository.UpdateAsync(old, cancellationToken);
            
            return request.user;
        }
    }
}
