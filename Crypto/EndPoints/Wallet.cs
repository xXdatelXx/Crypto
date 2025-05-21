using System.Security.Cryptography;
using System.Text;
using Crypto.Application.Logic.Queries.Wallet;
using Crypto.Data.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Crypto.EndPoints;

[Route("api/[controller]")]
public sealed class Wallet(IMediator mediator) : ControllerBase {
    [HttpGet, Route("GetWallet")]
    public async Task<IActionResult> GetWalletAsync(string telegramId) =>
        Ok(await mediator.Send(new GetWalletQuery(telegramId)));
}