using Crypto.Queries.Queries.GreedFear;
using Crypto.Telegram.MessageResponseHandler.Realisations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Crypto.EndPoints;

[ApiController]
public sealed class GreedFear(IMediator mediator) : ControllerBase {
   [HttpGet(ApiEndpoints.GreedFear.Get)]
   [ProducesResponseType(typeof(GreedFearResponse), StatusCodes.Status200OK)]
   [ProducesResponseType(StatusCodes.Status500InternalServerError)]
   public async Task<IActionResult> Get() {
      return Ok(await mediator.Send(new GreedFearQuery()));
   }
}