using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FamilyBudget.Controllers
{
    public class HomeController : ControllerBase
    {
        [Authorize(Roles = "admin, user")]
        public IActionResult Index()
        {
            string role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
            return Content($"Your role is: {role}");
        }

        [Authorize(Roles = "admin")]
        public IActionResult About()
        {
            return Content("Login only for administrator");
        }
    }
}
