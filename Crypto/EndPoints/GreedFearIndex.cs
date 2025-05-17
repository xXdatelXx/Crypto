using Crypto.Application.Logic;
using Crypto.Application.Logic.Queries.GreedFear;
using Crypto.Application.Logic.Queries.Price;
using Crypto.Application.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Crypto.EndPoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class GreedFearIndex(IMediator mediator) : ControllerBase
    {
        [HttpGet, Route("GetGreedFearIndex")]
        public async Task<IActionResult> GetIndex() =>
           Ok(await mediator.Send(new GreedFearQuery()));
    }
}
