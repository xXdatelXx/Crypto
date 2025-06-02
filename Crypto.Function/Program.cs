using Crypto.Application.Logic.Commands.User.SendTrackingCurrencies;
using Crypto.Application.Logic.Commands.User.Update;
using Crypto.Data;
using Crypto.Data.Interface;
using Crypto.Data.Repository;
using Crypto.Telegram;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices((builder, services) =>
    {
        services.AddDbContext<CryptoDBContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
           services.AddSingleton<ITelegramBotClient>(new TelegramBotClient(
                    builder.Configuration["TelegramBotToken"]
            ));
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddHttpClient();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<ICurrencyRepository, CurrencyRepository>();
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(SendTrackingCurrenciesCommand).Assembly);
        });
        services.AddTransient<IBot, Bot>();
    })
    .Build();

host.Run();
