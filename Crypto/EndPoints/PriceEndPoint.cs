using Crypto.Application.Logic;
using Crypto.Application.Logic.Queries.GreedFear;
using Crypto.Application.Logic.Queries.Price;
using Crypto.Application.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Crypto.EndPoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceEndPoint(IMediator mediator) : ControllerBase {
        
        [HttpGet, Route("GetPrice")]
        public async Task<IActionResult> GetPriceAsync(string currency, DateTime? time = null) => 
            Ok(await mediator.Send(new GetPriceQuery(currency, time)));

        [HttpGet, Route("GetDifference")]
        public async Task<IActionResult> GetDifference(string currency, DateTime time)
        {
            float old = await mediator.Send(new GetPriceQuery(currency, time));
            float current = await mediator.Send(new GetPriceQuery(currency));
            float difference = current - old;
            float percent = difference / old * 100;

            return Ok(new DifferenceDto()
            {
                Symbol = currency,
                Time = time,
                OldPrice = old,
                CurrentPrice = current,
                Difference = difference,
                PercentChange = Math.Round(percent, 2)
            });
        }

        [HttpGet, Route("GetGreedFearIndex")]
        public async Task<IActionResult> GetGreedFearIndex() => 
            Ok(await mediator.Send(new GreedFearQuery()));

        // POST api/<PriceEndPoint>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PriceEndPoint>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PriceEndPoint>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
