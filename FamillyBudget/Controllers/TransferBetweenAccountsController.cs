using FamilyBudget.Filters;
using FamilyDto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountsStatements.Services;
using Microsoft.AspNetCore.Authorization;

namespace FamilyBudget.Controllers
{
    [Route("users/{userId}/accounts/{fromAccountId}/{toAccountId}")]
    [ApiExceptionFilter]
    [Authorize]
    public class TransferBetweenAccountsController : ControllerBase
    {
        private readonly TransferBetweenAccounts _withAccount;

        public TransferBetweenAccountsController(TransferBetweenAccounts withAccount)
        {
            _withAccount = withAccount;
        }

        [HttpPost]
        public async Task<IActionResult> AccountTrancferAsync([FromRoute] int userId, [FromRoute] int fromAccountId, [FromRoute] int toAccountId, [FromBody] AccountStatementDto accountStatementDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _withAccount.TransferAccountAsync(userId, fromAccountId, toAccountId, accountStatementDto);

            return Ok();
        }
    }
}
