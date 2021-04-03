using FamilyBudget.Filters;
using FamilyDto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountsStatements.Services;

namespace FamilyBudget.Controllers
{
    [Route("{fromAccountId}/{toAccountId}")]
    [ApiExceptionFilter]
    public class AccountStatementController : ControllerBase
    {
        private readonly TransferBetweenAccounts _withAccount;

        public AccountStatementController(TransferBetweenAccounts withAccount)
        {
            _withAccount = withAccount;
        }

        [HttpPost]
        public async Task<IActionResult> AccountOperationAsync([FromRoute] int fromAccountId, [FromRoute] int toAccountId, [FromBody] AccountStatementDto accountStatementDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _withAccount.OperationAccountAsync(fromAccountId, toAccountId, accountStatementDto);

            return Ok();
        }
    }
}
