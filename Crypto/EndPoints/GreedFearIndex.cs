using Crypto.Queries.Queries.GreedFear;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Crypto.EndPoints;

[Route("api/[controller]"), ApiController]
public sealed class GreedFearIndex(IMediator mediator) : ControllerBase {
   [HttpGet, Route("GetGreedFearIndex")]
   public async Task<IActionResult> GetIndex() {
      return Ok(await mediator.Send(new GreedFearQuery()));
   }
}