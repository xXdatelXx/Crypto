using Crypto.Application.Logic.Commands;
using Crypto.Application.Logic.Queries.GreedFear;
using Crypto.Application.Logic.Queries.Price;
using Crypto.Data;
using Crypto.Data.Interface;
using Crypto.Data.Repository;
using Crypto.Telegram;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

builder.Services.AddDbContext<CryptoDBContext>(options =>
   options.UseNpgsql("Host=localhost;Database=CryptoDb;Username=postgres;Password=1"));

builder.Services.AddMediatR(cfg => {
   cfg.RegisterServicesFromAssembly(typeof(GetPriceQueryHandler).Assembly);
   cfg.RegisterServicesFromAssembly(typeof(GreedFearQueryHandler).Assembly);
   cfg.RegisterServicesFromAssembly(typeof(UpdateUserCommandHandler).Assembly);
});

builder.Services.AddScoped<ICurrencyRepository, CurrencyRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddSingleton<ITelegramBotClient>(new TelegramBotClient("8040659146:AAEeJELy6WOw9PJPiEi-PIXdJpOKHzzNOVw"));

builder.Services.AddHostedService<Bot>();

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
   app.UseSwagger();
   app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();