using Crypto.Queries.Model;
using Crypto.Queries.Queries.Price;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Crypto.EndPoints;

[Route("api/[controller]"), ApiController]
public sealed class PriceEndPoint(IMediator mediator) : ControllerBase {
   [HttpGet, Route("GetPrice")]
   public async Task<IActionResult> GetPriceAsync(string currency, DateTime? time = null) {
      return Ok(await mediator.Send(new GetPriceQuery(currency, time)));
   }

   [HttpGet, Route("GetDifference")]
   public async Task<IActionResult> GetDifference(string currency, DateTime time) {
      float old = await mediator.Send(new GetPriceQuery(currency, time));
      float current = await mediator.Send(new GetPriceQuery(currency));
      float difference = current - old;
      float percent = difference / old * 100;

      return Ok(new DifferenceModel {
         Symbol = currency,
         Time = time,
         OldPrice = old,
         CurrentPrice = current,
         Difference = difference,
         PercentChange = Math.Round(percent, 2)
      });
   }
}