using Microsoft.AspNetCore.Mvc;
using Deposits.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FamilyBudget.Filters;
using FamilyDto;

namespace FamilyBudget.Controllers
{
    [Route("users/{userId}/deposits")]
    [ApiExceptionFilter]
    public class DepositController : ControllerBase
    {
        private readonly DepositService _service;

        public DepositController(DepositService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDepositsForUser([FromRoute]int userId, [FromBody]DepositDto depositDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var newDeposit = await _service.CreateDepositForUserAsync(userId, depositDto);
            return Ok(newDeposit);
        }

        [HttpGet("{depositId}")]
        public async Task<IActionResult> DepositGetAsync([FromRoute] int userId, [FromRoute]int depositId)
        {
            return Ok(await _service.GetDepositAsync(userId, depositId));
        }

        [HttpGet]
        public async Task<IEnumerable<DepositDto>> GetAllDepositsForUserAsync([FromRoute]int userId)
        {
            return await _service.GetAllDepositsForUserAsync(userId);
        }

        [HttpPatch("{depositId}")]
        public async Task<IActionResult> DepositPatchAsync([FromRoute]int userId, [FromBody]DepositDto depositDto, [FromRoute]int depositId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            depositDto.Id = depositId;
            var newDeposit = await _service.PatchDepositAsync(userId, depositDto);
            return Ok(newDeposit);
        }

        [HttpDelete("{depositId}")]
        public async Task<IActionResult> DepositDeleteAsync([FromRoute]int userId, [FromRoute]int depositid)
        {
            await _service.DeleteDepositAsync(userId, depositid);
            return Ok();
        }
    }
}
