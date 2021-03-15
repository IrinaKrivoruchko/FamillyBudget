using Microsoft.AspNetCore.Mvc;
using Users.Services;
using FamilyDto;
using System.Threading.Tasks;

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
    }
}
