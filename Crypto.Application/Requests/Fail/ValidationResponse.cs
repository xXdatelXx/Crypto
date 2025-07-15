namespace Crypto.Application.Requests.Fail;

public class ValidationResponse
{
   public required string PropertyName { get; init; }

   public required string Message { get; init; }
}