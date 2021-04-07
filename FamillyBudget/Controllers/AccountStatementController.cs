using FamilyBudget.Filters;
using FamilyDto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AccountsStatements.Services;
using Microsoft.AspNetCore.Authorization;

namespace FamilyBudget.Controllers
{
    [Route("users/{userId}/accounts/{accountId}")]
    [ApiExceptionFilter]
    public class AccountStatementController : ControllerBase
    {
        private readonly AccountStatementService _statementAccount;

        public AccountStatementController(AccountStatementService statementAccount)
        {
            _statementAccount = statementAccount;
        }

        [HttpPost]
        public async Task<IActionResult> AccountOperationAsync([FromRoute]int userId,[FromRoute] int accountId, [FromBody] AccountStatementDto accountStatementDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _statementAccount.AccountOperationAsync(userId, accountId, accountStatementDto);

            return Ok();
        }
    }
}
