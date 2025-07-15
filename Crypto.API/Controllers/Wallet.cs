using Crypto.Queries.Model;
using Crypto.Queries.Queries.Wallet;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Crypto.EndPoints;

[ApiController]
public sealed class Wallet(IMediator mediator) : ControllerBase {
   [HttpGet(ApiEndpoints.Wallet.Get)]
   [ProducesResponseType(typeof(WalletResponse), StatusCodes.Status200OK)]
   [ProducesResponseType(StatusCodes.Status404NotFound)]
   [ProducesResponseType(StatusCodes.Status500InternalServerError)]
   public async Task<IActionResult> GetWalletAsync(string telegramId) {
      return Ok(await mediator.Send(new GetWalletQuery(telegramId)));
   }
}