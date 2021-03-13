using FamilyDto;
using Microsoft.AspNetCore.Mvc;
using Wallets.Services;

namespace FamilyBudget.Controllers
{
    [Controller]
    [Route("users/{id}/wallet")]
    public class Wallet : ControllerBase
    {
        private readonly WalletService _service;

        public Wallet(WalletService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult WalletCreate(int id, [FromBody]WalletDto walletDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var createdWallet = _service.CreateWallet(id, walletDto);
            return Ok(createdWallet);
        }
    }
}
