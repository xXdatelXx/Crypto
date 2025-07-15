using Crypto.Application.Logic.Commands.Currency.Create;
using Crypto.Application.Logic.Commands.Currency.Remove;
using Crypto.Application.Logic.Commands.Currency.Update;
using Crypto.Application.Requests.Currency;
using Crypto.Application.Requests.Fail;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Crypto.EndPoints;

[ApiController]
public sealed class Currency(IMediator mediator) : ControllerBase {
   [HttpPost(ApiEndpoints.Currencies.Create)]
   [ProducesResponseType(typeof(Guid), statusCode: StatusCodes.Status201Created)]
   [ProducesResponseType(typeof(ValidationFailureResponse), StatusCodes.Status400BadRequest)]
   [ProducesResponseType(StatusCodes.Status500InternalServerError)]
   public async Task<IActionResult> Create(string name, CancellationToken token = default) {
      return Ok(await mediator.Send(new CreateCurrencyCommand(name), token));
   }

   [HttpPut(ApiEndpoints.Currencies.Update)]
   [ProducesResponseType(typeof(CurrencyResponse), statusCode: StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ValidationFailureResponse), StatusCodes.Status400BadRequest)]
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
   [ProducesResponseType(StatusCodes.Status500InternalServerError)]
   public async Task<IActionResult> Update(UpdateCurrencyCommand command, CancellationToken token = default) {
      return Ok(await mediator.Send(command, token));
   }

   [HttpDelete(ApiEndpoints.Currencies.Delete)]
   [ProducesResponseType(StatusCodes.Status200OK)]
   [ProducesResponseType(StatusCodes.Status404NotFound)]
   [ProducesResponseType(StatusCodes.Status500InternalServerError)]
   public async Task<IActionResult> Delete(Guid id, CancellationToken token = default) {
      return Ok(await mediator.Send(new RemoveCurrencyCommand(id), token));
   }
}