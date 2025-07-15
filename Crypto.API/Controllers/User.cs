using Crypto.Application.Logic.Commands.User.Create;
using Crypto.Application.Logic.Commands.User.Remove;
using Crypto.Application.Logic.Commands.User.Update;
using Crypto.Queries.Model;
using Crypto.Queries.Model.Get.Id;
using Crypto.Queries.Queries.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Crypto.EndPoints;

[ApiController]
public sealed class User(IMediator mediator) : ControllerBase {
   [HttpPost(ApiEndpoints.Users.Create)]
   [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
   [ProducesResponseType(StatusCodes.Status500InternalServerError)]
   public async Task<IActionResult> Create([FromBody] CreateUserCommand command, CancellationToken token) {
      var response = await mediator.Send(command, token);
      return CreatedAtAction(nameof(Create), response);
   }

   [HttpGet(ApiEndpoints.Users.Get)]
   [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
   [ProducesResponseType(StatusCodes.Status404NotFound)]
   [ProducesResponseType(StatusCodes.Status500InternalServerError)]
   public async Task<IActionResult> GetUser(Guid id, CancellationToken token) {
      return Ok(await mediator.Send(new GetUserByIdQuery(id), token));
   }
   
   [HttpGet(ApiEndpoints.Users.GetByTelegram)]
   [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
   [ProducesResponseType(StatusCodes.Status404NotFound)]
   [ProducesResponseType(StatusCodes.Status500InternalServerError)]
   public async Task<IActionResult> Get(string telegramId, CancellationToken token) {
      return Ok(await mediator.Send(new GetUserByTelegramIdQuery(telegramId), token));
   }

   [HttpPut(ApiEndpoints.Users.Update)]
   [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
   [ProducesResponseType(StatusCodes.Status500InternalServerError)]
   public async Task<IActionResult> Update(UpdateUserCommand command, CancellationToken token) {
      return Ok(await mediator.Send(command, token));
   }

   [HttpDelete(ApiEndpoints.Users.Delete)]
   [ProducesResponseType(StatusCodes.Status200OK)]
   [ProducesResponseType(StatusCodes.Status404NotFound)]
   [ProducesResponseType(StatusCodes.Status500InternalServerError)]
   public async Task<IActionResult> Delete(Guid id, CancellationToken token) {
      return Ok(await mediator.Send(new RemoveUserCommand(id), token));
   }
   
   /*ToDo: придумати як зробить 
   [HttpGet(ApiEndpoints.Users.Get)]
   public async Task<IActionResult> SendTrackingCurrencies(CancellationToken token) {
      return Ok(await mediator.Send(new SendTrackingCurrenciesCommand(), token));
   }*/
   
}