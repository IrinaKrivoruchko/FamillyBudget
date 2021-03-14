using FamilyDto;
using Microsoft.AspNetCore.Mvc;
using Wallets.Services;

namespace FamilyBudget.Controllers
{
    [Route("users/{id}/wallet")]
    public class WalletController : ControllerBase
    {
        private readonly WalletService _service;

        public WalletController(WalletService service)
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
