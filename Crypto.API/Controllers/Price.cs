using Crypto.Queries.Model;
using Crypto.Queries.Queries.Price;
using Crypto.Queries.Queries.Price.Difference;
using Crypto.Telegram.MessageResponseHandler.Realisations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Crypto.EndPoints;

[ApiController]
public sealed class Price(IMediator mediator) : ControllerBase {
   [HttpGet(ApiEndpoints.Prices.Get)]
   [ProducesResponseType(typeof(PriceResponse), StatusCodes.Status200OK)]
   [ProducesResponseType(StatusCodes.Status404NotFound)]
   [ProducesResponseType(StatusCodes.Status500InternalServerError)]
   public async Task<IActionResult> Get(string currency, DateTime? time = null) {
      return Ok(await mediator.Send(new GetPriceQuery(currency, time)));
   }

   [HttpGet(ApiEndpoints.Prices.GetDifference)]
   [ProducesResponseType(typeof(DifferencePriceResponse), StatusCodes.Status200OK)]
   [ProducesResponseType(StatusCodes.Status404NotFound)]
   [ProducesResponseType(StatusCodes.Status500InternalServerError)]
   public async Task<IActionResult> GetDifference(string currency, DateTime time) {
      return Ok(await mediator.Send(new GetPriceDifferenceQuery(currency, time)));
   }
}