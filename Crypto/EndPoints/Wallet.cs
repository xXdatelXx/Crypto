using Crypto.Application.Logic.Queries.Wallet;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Crypto.EndPoints;

[Route("api/[controller]")]
public sealed class Wallet(IMediator mediator) : ControllerBase {
   [HttpGet, Route("GetWallet")]
   public async Task<IActionResult> GetWalletAsync(string telegramId) {
      return Ok(await mediator.Send(new GetWalletQuery(telegramId)));
   }
}