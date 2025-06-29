using Crypto.Data.Interface;
using Crypto.Data.Repository;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Crypto.Data.Configuration;

public static class ServiceCollectionExtensions {
   public static IServiceCollection AddData(this IServiceCollection services)
   {
      services.AddScoped<ICurrencyRepository, CurrencyRepository>();
      services.AddScoped<IUserRepository, UserRepository>();
      services.AddValidatorsFromAssemblyContaining<DataAssemblyMarker>(ServiceLifetime.Singleton);
      
      return services;
   }
   
   public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
   {
      services.AddDbContext<CryptoDBContext>(options => options.UseNpgsql(connectionString));
      return services;
   }
}