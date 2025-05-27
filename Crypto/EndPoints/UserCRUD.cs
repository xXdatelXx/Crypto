using Crypto.Application.Logic.Commands.User.Create;
using Crypto.Application.Logic.Commands.User.Remove;
using Crypto.Application.Logic.Commands.User.Update;
using Crypto.Application.Model;
using Crypto.Queries.Queries.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Crypto.EndPoints;

[Route("api/[controller]"), ApiController]
public sealed class UserCRUD(IMediator mediator) : ControllerBase {
   [HttpPost, Route("CreateUser")]
   public async Task<IActionResult> CreateUser(string telegramId, string bybitKey, string bybitSicret, CancellationToken token = default) {
      return Ok(await mediator.Send(new CreateUserCommand(telegramId, bybitKey, bybitSicret), token));
   }

   [HttpGet, Route("GetUser")]
   public async Task<IActionResult> GetUser(string telegramId, CancellationToken token = default) {
      return Ok(await mediator.Send(new GetUserByTGIdQuery(telegramId), token));
   }

   [HttpPost, Route("UpdateUser")]
   public async Task<IActionResult> UpdateUser(
      Guid id,
      string telegramId,
      string bybitKey,
      string bybitSicret,
      IEnumerable<string> currencies, CancellationToken token = default) {
      UserDTO user = new() {
         Id = id,
         TelegramId = telegramId,
         ByBitApiKey = bybitKey,
         ByBitApiSicret = bybitSicret,
         Currencies = currencies
      };

      return Ok(await mediator.Send(new UpdateUserCommand(user), token));
   }

   [HttpDelete, Route("RemoveUser")]
   public async Task<IActionResult> RemoveUser(Guid id, CancellationToken token = default) {
      return Ok(await mediator.Send(new RemoveUserCommand(id), token));
   }
}