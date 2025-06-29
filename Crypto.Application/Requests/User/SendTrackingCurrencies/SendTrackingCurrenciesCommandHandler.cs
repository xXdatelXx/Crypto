using Crypto.Data.Interface;
using MediatR;
using Crypto.Telegram;
using Microsoft.Extensions.Configuration;

namespace Crypto.Application.Logic.Commands.User.SendTrackingCurrencies;

public class SendTrackingCurrenciesCommandHandler(
    IUserRepository repository, 
    IBot bot, 
    IHttpClientFactory httpClientFactory,
    IConfiguration configuration) : IRequestHandler<SendTrackingCurrenciesCommand, Unit>
{
    public async Task<Unit> Handle(SendTrackingCurrenciesCommand request, CancellationToken cancellationToken)
    {
        var http = httpClientFactory.CreateClient();
        http.BaseAddress = new Uri(configuration["ApiBaseAddress"]);
            
        var users = await repository.GetAllAsync(cancellationToken);
        if(users == null)
            throw new ArgumentException("Kozel");

        foreach (var user in users) 
            await bot.SendMessageAsync(user.TelegramId, "/tracking", cancellationToken);
            
        return Unit.Value;
    }
}