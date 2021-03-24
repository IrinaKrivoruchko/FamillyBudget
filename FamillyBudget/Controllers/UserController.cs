using Microsoft.AspNetCore.Mvc;
using Users.Services;
using FamilyDto;
using System.Threading.Tasks;
using System.Linq;

namespace FamilyBudget.Controllers
{
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly UserService _service;

        public UserController(UserService service)
        {
            _service = service;
        }

        [HttpGet]
        public IQueryable<UserDto> UserAllGet()
        {
            return _service.GetAllUser();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> UserGetAsync(UserDto userDto, int id)
        {
            userDto.Id = id;
            var getUser = await _service.GetUserAsync(id);
            return Ok(getUser);
        }

        [HttpPost]
        public async Task<IActionResult> UserCreate([FromBody]UserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var createdUser = await _service.CreateUserAsync(userDto);
            return Ok(createdUser);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> UserDelete(int id)
        {
            await _service.DeleteUserAsync(id);
            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UserPatch(UserDto userDto, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            userDto.Id = id;
            var newUser = await _service.PatchUserAsync(userDto);
            return Ok(newUser);
        }
    }
}
