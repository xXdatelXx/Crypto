using Crypto.Application.Logic.Commands;
using Crypto.Application.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Crypto.EndPoints;

[Route("api/[controller]")]
[ApiController]
public class UserCRUD(IMediator mediator) : ControllerBase
{
    [HttpGet, Route("CreateUser")]
    public async Task<IActionResult> CreateUser(string telegramId, string bybitKey, string bybitSicret, CancellationToken token = default)
    {
        return Ok(await mediator.Send(new CreateUserCommand(telegramId, bybitKey, bybitSicret), token));
    }

    [HttpPost, Route("UpdateUser")]
    public async Task<IActionResult> UpdateUser(Guid id, string telegramId, string bybitKey, string bybitSicret, CancellationToken token = default)
    {
        UserDTO user = new()
        {
            Id = id,
            TelegramId = telegramId,
            ByBitApiKey = bybitKey,
            ByBitApiSicret = bybitSicret
        };

        return Ok(await mediator.Send(new UpdateUserCommand(user), token));
    }

    [HttpDelete, Route("RemoveUser")]
    public async Task<IActionResult> RemoveUser(Guid id, CancellationToken token = default)
    {
        return Ok(await mediator.Send(new RemoveUserCommand(id), token));
    }
}