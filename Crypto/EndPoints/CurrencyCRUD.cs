using Crypto.Application.Logic.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Crypto.EndPoints;

[Route("api/[controller]"), ApiController]
public sealed class CurrencyCRUD(IMediator mediator) : ControllerBase {
   [HttpPost, Route("CreateCurrency")]
   public async Task<IActionResult> CreateCurrency(string name, CancellationToken token = default) {
      return Ok(await mediator.Send(new CreateCurrencyCommand(name), token));
   }

   [HttpPut, Route("UpdateCurrency")]
   public async Task<IActionResult> UpdateCurrency(Guid id, string name, CancellationToken token = default) {
      CurrencyDTO currency = new() {
         Id = id,
         Name = name
      };

      return Ok(await mediator.Send(new UpdateCurrencyCommand(currency), token));
   }

   [HttpDelete, Route("RemoveCurrency")]
   public async Task<IActionResult> RemoveCurrency(Guid id, CancellationToken token = default) {
      return Ok(await mediator.Send(new RemoveCurrencyCommand(id), token));
   }
}