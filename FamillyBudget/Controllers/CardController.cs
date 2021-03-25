using Cards.Services;
using FamilyBudget.Filters;
using FamilyDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyBudget.Controllers
{
    [Route("users/{userId}/cards")]
    [ApiExceptionFilter]
    public class CardController : ControllerBase
    {
        private readonly CardService _service;

        public CardController(CardService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IQueryable<CardDto>> AllCardsForUserGet(int userId)
        {
            return await _service.GetAllCardsForUser(userId);
        }

        [HttpGet("{cardId}")]
        public async Task<IActionResult> CardGetAsync(int userId, int cardId)
        {
            return Ok(await _service.GetCardAsync(userId, cardId));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCardsForUser(int userId, [FromBody] CardDto cardDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var createdCard = await _service.CreateCardsForUserAsync(userId, cardDto);
            return Ok(createdCard);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> CardDelete([FromRoute]int userId, int id)
        {
            await _service.DeleteCardAsync(userId, id);
            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> CardPatch([FromRoute]int userId, [FromBody]CardDto cardDto, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            cardDto.Id = id;
            var newCard = await _service.PatchCardAsync(userId, cardDto);
            return Ok(newCard);
        }
    }
}
