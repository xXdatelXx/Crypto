using Crypto.Application.Logic.Commands.User.Create;
using Crypto.Application.Logic.Commands.User.Remove;
using Crypto.Application.Logic.Commands.User.SendTrackingCurrencies;
using Crypto.Application.Logic.Commands.User.Update;
using Crypto.Application.Model;
using Crypto.Queries.Queries.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Crypto.EndPoints;

[ApiController]
public sealed class User(IMediator mediator) : ControllerBase {
   [HttpPost(ApiEndpoints.Users.Create)]
   public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request, CancellationToken token) {
      var guid = await mediator.Send(new CreateUserCommand(request), token);
      return CreatedAtAction(nameof(CreateUser), new { id = guid }, movieResponse);
      return Ok(await mediator.Send(new CreateUserCommand(request), token));
   }

   [HttpGet(ApiEndpoints.Users.Get)]
   public async Task<IActionResult> GetUser([FromRoute] Guid guid, CancellationToken token) {
      return Ok(await mediator.Send(new GetUserByIdQuery(guid), token));
   }
   
   
   [HttpGet(ApiEndpoints.Users.Get)]
   public async Task<IActionResult> GetUser([FromRoute] string telegramId, CancellationToken token) {
      return Ok(await mediator.Send(new GetUserByTGIdQuery(telegramId), token));
   }

   [HttpPost(ApiEndpoints.Users.Update)]
   public async Task<IActionResult> UpdateUser([FromRoute]Guid id, [FromBody]UpdateUserRequest request, CancellationToken token) {
      return Ok(await mediator.Send(new UpdateUserCommand(id, request), token));
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