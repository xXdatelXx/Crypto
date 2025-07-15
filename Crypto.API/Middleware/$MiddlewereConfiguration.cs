namespace Crypto.Middleware;

public static class MiddlewereConfiguration {
   public static IServiceCollection AddMiddleware(this IServiceCollection services) {
      services.AddExceptionHandler<ExceptionLogger>();
      services.AddExceptionHandler<GlobalExceptionHandler>();
      services.AddExceptionHandler<ValidationExceptionHandler>();
      services.AddProblemDetails();

      return services;
   }
}