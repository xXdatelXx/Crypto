using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Crypto.EndPoints;

[Route("api/[controller]")]
public sealed class Wallet : ControllerBase {
   [HttpPost, Route("GetWallet")]
   public async Task<IActionResult> GetWalletAsync() {
      return null;
   }
}