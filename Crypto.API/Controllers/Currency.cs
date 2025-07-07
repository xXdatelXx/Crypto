using Crypto.Application.Logic.Commands.Currency.Create;
using Crypto.Application.Logic.Commands.Currency.Remove;
using Crypto.Application.Logic.Commands.Currency.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Crypto.EndPoints;

[ApiController]
public sealed class Currency(IMediator mediator) : ControllerBase {
   [HttpPost(ApiEndpoints.Currencies.Create)]
   public async Task<IActionResult> Create(string name, CancellationToken token = default) {
      return Ok(await mediator.Send(new CreateCurrencyCommand(name), token));
   }

   [HttpPost, Route("UpdateCurrency")]
   public async Task<IActionResult> UpdateCurrency(Guid id, string name, CancellationToken token = default) {
      var command = new UpdateCurrencyCommand(id, name, new List<Guid>());
      return Ok(await mediator.Send(command, token));
   }

   [HttpDelete, Route("RemoveCurrency")]
   public async Task<IActionResult> RemoveCurrency(Guid id, CancellationToken token = default) {
      return Ok(await mediator.Send(new RemoveCurrencyCommand(id), token));
   }
}