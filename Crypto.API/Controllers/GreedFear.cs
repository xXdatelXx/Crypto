using Crypto.Queries.Queries.GreedFear;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Crypto.EndPoints;

[ApiController]
public sealed class GreedFear(IMediator mediator) : ControllerBase {
   [HttpGet(ApiEndpoints.GreedFear.Get)]
   public async Task<IActionResult> Get() {
      return Ok(await mediator.Send(new GreedFearQuery()));
   }
}