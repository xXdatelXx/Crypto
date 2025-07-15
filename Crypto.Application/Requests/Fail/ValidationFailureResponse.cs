namespace Crypto.Application.Requests.Fail;

public sealed class ValidationFailureResponse {
   public required IEnumerable<ValidationResponse> Errors { get; init; }
}