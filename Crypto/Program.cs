using Crypto.Application.Logic.Queries.GreedFear;
using Crypto.Application.Logic.Queries.Price;
using Crypto.Data;
using Crypto.Data.Interface;
using Crypto.Data.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CryptoDBContext>(options =>
    options.UseNpgsql("Host=localhost;Database=CryptoDb;Username=postgres;Password=1"));
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(GetPriceQueryHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(GreedFearQueryHandler).Assembly);
});

builder.Services.AddScoped<ICurrencyRepository, CurrencyRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
