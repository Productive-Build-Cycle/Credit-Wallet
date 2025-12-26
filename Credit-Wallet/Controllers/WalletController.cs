using Credit_Wallet.Features.MakeWallet;
using Microsoft.AspNetCore.Mvc;

namespace Credit_Wallet.Controllers;

[ApiController]
[Route("api/wallet")]
public class WalletController : ControllerBase
{
    private readonly IMakeWalletService _makeWalletService;

    public WalletController(IMakeWalletService makeWalletService)
    {
        _makeWalletService = makeWalletService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateWallet()
    {
        var resultId = await _makeWalletService.HandleAsync();
        
        return Ok(new {message = "Wallet created successfully"});
    }
}