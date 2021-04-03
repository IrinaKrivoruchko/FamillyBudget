using Accounts.Services;
using FamilyBudget.Filters;
using FamilyDto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyBudget.Controllers
{
    [Route("users/{accountId}/accpunts")]
    [ApiExceptionFilter]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _service;

        public AccountController(AccountService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IQueryable<AccountDto>> AllAccountsForUserGet(int userId)
        {
            return await _service.GetAllAccountsForUser(userId);
        }

        [HttpGet("{accountId}")]
        public async Task<IActionResult> AccountGetAsync(int userId, int accountId)
        {
            return Ok(await _service.GetAccountAsync(userId, accountId));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccountsForUser(int userId, [FromBody] AccountDto accountDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var createdAccount = await _service.CreateAccountsForUserAsync(userId, accountDto);
            return Ok(createdAccount);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> AccountDelete([FromRoute] int userId, int id)
        {
            await _service.DeleteAccountAsync(userId, id);
            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> AccountPatch([FromRoute] int userId, [FromBody] AccountDto accountDto, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            accountDto.Id = id;
            var newAccount = await _service.PatchAccountAsync(userId, accountDto);
            return Ok(newAccount);
        }
    }
}
