using Crypto.Application.Logic.Queries.GreedFear;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Crypto.EndPoints;

[Route("api/[controller]"), ApiController]
public class GreedFearIndex(IMediator mediator) : ControllerBase {
   [HttpGet, Route("GetGreedFearIndex")]
   public async Task<IActionResult> GetIndex() {
      return Ok(await mediator.Send(new GreedFearQuery()));
   }
}