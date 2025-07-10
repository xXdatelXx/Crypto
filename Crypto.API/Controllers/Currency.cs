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

   [HttpPut(ApiEndpoints.Currencies.Update)]
   public async Task<IActionResult> UpdateCurrency(UpdateCurrencyCommand command, CancellationToken token = default) {
      return Ok(await mediator.Send(command, token));
   }

   [HttpDelete(ApiEndpoints.Currencies.Delete)]
   public async Task<IActionResult> RemoveCurrency(Guid id, CancellationToken token = default) {
      return Ok(await mediator.Send(new RemoveCurrencyCommand(id), token));
   }
}