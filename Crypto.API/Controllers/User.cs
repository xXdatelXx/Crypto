using Crypto.Application.Logic.Commands.User.Create;
using Crypto.Application.Logic.Commands.User.Remove;
using Crypto.Application.Logic.Commands.User.Update;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Crypto.EndPoints;

[ApiController]
public sealed class User(IMediator mediator) : ControllerBase {
   [HttpPost(ApiEndpoints.Users.Create)]
   public async Task<IActionResult> Create([FromBody] CreateUserCommand command, CancellationToken token) {
      var response = await mediator.Send(command, token);
      return CreatedAtAction(nameof(Create), response);
   }

   /*[HttpGet(ApiEndpoints.Users.Get)]
   public async Task<IActionResult> GetUser([FromRoute] Guid guid, CancellationToken token) {
      return Ok(await mediator.Send(new GetUserByTGIdQuery(guid), token));
   }*/
   
   
   /*[HttpGet(ApiEndpoints.Users.Get)]
   public async Task<IActionResult> GetUser([FromRoute] string telegramId, CancellationToken token) {
      return Ok(await mediator.Send(new GetUserByTGIdQuery(telegramId), token));
   }*/

   [HttpPost(ApiEndpoints.Users.Update)]
   public async Task<IActionResult> UpdateUser([FromBody]UpdateUserCommand command, CancellationToken token) {
      return Ok(await mediator.Send(command, token));
   }

   [HttpDelete(ApiEndpoints.Users.Delete)]
   public async Task<IActionResult> DeleteUser([FromRoute] Guid id, CancellationToken token) {
      return Ok(await mediator.Send(new RemoveUserCommand(id), token));
   }
   
   /*ToDo: придумать як зробить 
   [HttpGet(ApiEndpoints.Users.Get)]
   public async Task<IActionResult> SendTrackingCurrencies(CancellationToken token) {
      return Ok(await mediator.Send(new SendTrackingCurrenciesCommand(), token));
   }*/
   
}