using Crypto.Application.Configuration;
using Crypto.Data.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Configuration.AddUserSecrets<Program>();

// Extensions
builder.Services
   .AddMediatR()
   .AddApplication()
   .AddDatabase(builder.Configuration.GetConnectionString("DefaultConnection")!)
   .AddData();
   //.AddTelegram(
   //   builder.Configuration["TelegramBotToken"]!, 
   //   builder.Configuration["ApiBaseAddress"]!);

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
   app.UseSwagger();
   app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
