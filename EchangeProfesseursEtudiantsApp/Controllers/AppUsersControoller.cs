using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using EchangeProfesseursEtudiantsApp.Models;

namespace EchangeProfesseursEtudiantsApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AppUsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AppUsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }


        [HttpGet]
        public IActionResult ListUsers()
        {
            var users = _userManager.Users;
            return View(users);
        }

    }
}
