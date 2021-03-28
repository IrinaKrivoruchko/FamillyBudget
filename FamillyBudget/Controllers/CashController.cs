using FamilyBudget.Filters;
using Microsoft.AspNetCore.Mvc;
using FamilyDto;
using Cashes.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace FamilyBudget.Controllers
{
    [Route("users/{userId}/cashes")]
    [ApiExceptionFilter]
    public class CashController : ControllerBase
    {
        private readonly CashService _service;

        public CashController(CashService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCashesForUser(int userId, [FromBody]CashDto cashDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            var newCash = await _service.CreateCashesForUserAsync(userId, cashDto);
            return Ok(newCash);
        }

        [HttpGet]
        public async Task<IEnumerable<CashDto>> GetAllCashesForUserAsync([FromRoute]int userId)
        {
            return await _service.GetAllCashesForUser(userId);
        }

        [HttpGet("{cashId}")]
        public async Task<IActionResult> CashGetAsync([FromRoute]int userId, int cashId)
        {
            return Ok(await _service.GetCashAsync(userId, cashId));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> CashDelete([FromRoute] int userId, int id)
        {
            await _service.DeleteCashAsync(userId, id);
            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> CashPatch([FromRoute] int userId, [FromBody] CashDto cashDto, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            cashDto.Id = id;
            var newCash = await _service.PatchCashAsync(userId, cashDto);
            return Ok(newCash);
        }
    }
}
