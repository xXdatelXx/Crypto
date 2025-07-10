using Crypto.Queries.Queries.Wallet;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Crypto.EndPoints;

[ApiController]
public sealed class Wallet(IMediator mediator) : ControllerBase {
   [HttpGet(ApiEndpoints.Wallet.Get)]
   public async Task<IActionResult> GetWalletAsync(string telegramId) {
      return Ok(await mediator.Send(new GetWalletQuery(telegramId)));
   }
}