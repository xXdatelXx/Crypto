using Crypto.Application.Logic.Commands.Currency.Create;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Crypto.Application.Configuration;

public static class ServiceCollectionExtensions {
   public static IServiceCollection AddMediatR(this IServiceCollection services) {
      services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationAssemblyMarker).Assembly));
      return services;
   }
   
   public static IServiceCollection AddApplication(this IServiceCollection services) {
      services.AddValidatorsFromAssemblyContaining<ApplicationAssemblyMarker>();
      return services;
   }
}