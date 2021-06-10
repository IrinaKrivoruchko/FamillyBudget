using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using DataStorage;
using FamilyDto;
using DataEntities;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Authorization.Services;

namespace FamilyBudget.Controllers
{
    [Route("authorization")]
    public class AuthorizationController : ControllerBase
    {
        private readonly DatabaseContext _dbContext;
        private readonly AuthorizationService _service;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthorizationController(DatabaseContext dbContext, AuthorizationService service, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _dbContext = dbContext;
            _service = service;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromBody]UserDto userDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var res = await _service.Token(userDto);
            return Ok(res);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
