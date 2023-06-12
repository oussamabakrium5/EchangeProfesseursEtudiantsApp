using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using EchangeProfesseursEtudiantsApp.Models;
using EchangeProfesseursEtudiantsApp.ViewModel;
using Microsoft.EntityFrameworkCore;
using EchangeProfesseursEtudiantsApp.Data;

namespace EchangeProfesseursEtudiantsApp.Controllers
{
    
    public class AppUsersController : Controller
    {
        private readonly ApplicationDbContext _context;

		public AppUsersController(ApplicationDbContext context)
        {
            _context = context;

		}


		[Authorize(Roles = "Administrator")]
		public async Task<IActionResult> Index()
        {





            var tables = new UsersViewModel
            {
                applicationusers = _context.applicationUsers.ToList()
            };
            return View(tables);





            /*return _context.Groups != null ? 
                        View(await _context.Groups.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Groups'  is null.");*/
        }

        /*public IActionResult Index()
        {
            var users = _userManager.Users;
            return View(users);
            *//*return _context.Groups != null ? 
                        View(await _context.Groups.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Groups'  is null.");*//*
        }

        [HTTPGet]
        public IActionResult ListUsers()
        {
            var users = _userManager.Users;
            return View(users);
        }
*/
    }
}
