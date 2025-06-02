using Crypto.Application.Logic.Commands.User.Update;
using Crypto.Data;
using Crypto.Data.Interface;
using Crypto.Data.Repository;
using Crypto.Queries.Queries.GreedFear;
using Crypto.Queries.Queries.Price;
using Crypto.Telegram;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Configuration.AddUserSecrets<Program>();

builder.Services.AddDbContext<CryptoDBContext>(options =>
   options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMediatR(cfg => {
   cfg.RegisterServicesFromAssembly(typeof(GetPriceQueryHandler).Assembly);
   cfg.RegisterServicesFromAssembly(typeof(GreedFearQueryHandler).Assembly);
   cfg.RegisterServicesFromAssembly(typeof(UpdateUserCommandHandler).Assembly);
});

builder.Services.AddScoped<ICurrencyRepository, CurrencyRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddSingleton<ITelegramBotClient>(new TelegramBotClient(
    builder.Configuration["TelegramBotToken"]
));
builder.Services.AddSingleton<IBot, Bot>();
builder.Services.AddHostedService<BotService>();

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
   app.UseSwagger();
   app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
