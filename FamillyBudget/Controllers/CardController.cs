using Cards.Services;
using FamilyBudget.Filters;
using FamilyDto;
using Microsoft.AspNetCore.Mvc;
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
        public IQueryable<CardDto> AllCardsOfUserGet(int userId)
        {
            return _service.GetAllCardsForUser(userId);
        }

        [HttpGet("{cardId}")]
        public async Task<IActionResult> CardGetAsync(int userId, int cardId, CardDto cardDto)
        {
            cardDto.Id = cardId;
            var getCard = await _service.GetCardAsync(userId, cardId);
            return Ok(getCard);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCardsForUser(int userId, [FromBody]CardDto cardDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var createdCard = await _service.CreateCardsForUserAsync(userId, cardDto);
            return Ok(createdCard);
        }
    }
}
