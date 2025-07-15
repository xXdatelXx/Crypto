using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Crypto.Middleware;

public sealed class ExceptionLogger (ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
   public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
   {
      logger.LogError(exception, "Unhandled exception occurred");
      return ValueTask.FromResult(false);
   }
}