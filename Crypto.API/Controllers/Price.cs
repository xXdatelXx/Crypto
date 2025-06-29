using Crypto.Queries.Model;
using Crypto.Queries.Queries.Price;
using Crypto.Queries.Queries.Price.Difference;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Crypto.EndPoints;

[Route("api/[controller]"), ApiController]
public sealed class Price(IMediator mediator) : ControllerBase {
   [HttpGet, Route("GetPrice")]
   public async Task<IActionResult> GetPriceAsync(string currency, DateTime? time = null) {
      return Ok(await mediator.Send(new GetPriceQuery(currency, time)));
   }

   [HttpGet, Route("GetDifference")]
   public async Task<IActionResult> GetDifference(string currency, DateTime time) {
      return Ok(await mediator.Send(new GetPriceDifferenceQuery(currency, time)));
   }
}