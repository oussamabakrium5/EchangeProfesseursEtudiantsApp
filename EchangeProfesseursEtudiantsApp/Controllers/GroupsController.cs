using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EchangeProfesseursEtudiantsApp.Data;
using EchangeProfesseursEtudiantsApp.Models;
using EchangeProfesseursEtudiantsApp.ViewModel;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace EchangeProfesseursEtudiantsApp.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class GroupsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GroupsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Groups
        public async Task<IActionResult> Index()
        {

            var tables = new GroupViewModel
            {
                Groups = _context.Groups.ToList(),
                applicationusers = _context.applicationUsers.ToList()
            };

            return View(tables);
            /*return _context.Groups != null ? 
                        View(await _context.Groups.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Groups'  is null.");*/
        }

        // GET: Groups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Groups == null)
            {
                return NotFound();
            }

            var @group = await _context.Groups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@group == null)
            {
                return NotFound();
            }

            var tables = new GroupViewModel
            {
                Groups = _context.Groups.ToList(),
                applicationusers = _context.applicationUsers.ToList()
            };

            tables.Groups.FirstOrDefault().Id = @group.Id;
            tables.Groups.FirstOrDefault().Name = @group.Name;
            tables.Groups.FirstOrDefault().Description = @group.Description;
            tables.Groups.FirstOrDefault().applicationuser = @group.applicationuser;
            tables.applicationusers.FirstOrDefault().Email = @group.applicationuser.Email;
            tables.applicationusers.FirstOrDefault().Firstname = @group.applicationuser.Firstname;
            tables.applicationusers.FirstOrDefault().Lastname = @group.applicationuser.Lastname;

            return View(tables);

            /*return View(@group);*/
        }

        // GET: Groups/Create
        public IActionResult Create()
        {

            var tables = new GroupViewModel
            {
                Groups = _context.Groups.ToList(),
                applicationusers = _context.applicationUsers.ToList()
            };

            return View(tables);
        }

        // POST: Groups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Group item, ApplicationUser user)
        {

            foreach (var categ in _context.applicationUsers)
            {
                if (categ.Email == user.Email)
                {
                    item.applicationuser = categ;
                    break;
                }
            }

            _context.Groups.Add(item);
            _context.SaveChanges();
            return RedirectToAction("Index");

            /*return RedirectToAction(nameof(Index));*/
        }

        // GET: Groups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Groups == null)
            {
                return NotFound();
            }

            var @group = await _context.Groups.FindAsync(id);
            if (@group == null)
            {
                return NotFound();
            }

            var tables = new GroupViewModel
            {
                Groups = _context.Groups.ToList(),
                applicationusers = _context.applicationUsers.ToList()
            };

            tables.Groups.FirstOrDefault().Id = @group.Id;
            tables.Groups.FirstOrDefault().Name = @group.Name;
            tables.Groups.FirstOrDefault().Description = @group.Description;
            tables.Groups.FirstOrDefault().applicationuser = @group.applicationuser;
            tables.applicationusers.FirstOrDefault().Email = @group.applicationuser.Email;

            return View(tables);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Group item, ApplicationUser user)
        {
            int i = 0;
            foreach (var categ in _context.applicationUsers)
            {
                if (item.applicationuser.Email ==categ.Email)
                {
                    item.applicationuser.Id = categ.Id;
                    item.applicationuser.Firstname = categ.Firstname;
                    item.applicationuser.Lastname = categ.Lastname;
                    item.applicationuser = categ;
                    i = 1;
                    break;
                }
            }

            if (i== 1)
            {
                _context.Groups.Update(item);
                _context.SaveChanges();
            }
            
            return RedirectToAction("Index");

            /*if (id != @group.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@group);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupExists(@group.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(@group);*/
        }

        // GET: Groups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Groups == null)
            {
                return NotFound();
            }

            var @group = await _context.Groups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@group == null)
            {
                return NotFound();
            }

            var tables = new GroupViewModel
            {
                Groups = _context.Groups.ToList(),
                applicationusers = _context.applicationUsers.ToList()
            };

            tables.Groups.FirstOrDefault().Id = @group.Id;
            tables.Groups.FirstOrDefault().Name = @group.Name;
            tables.Groups.FirstOrDefault().Description = @group.Description;
            tables.Groups.FirstOrDefault().applicationuser = @group.applicationuser;
            tables.applicationusers.FirstOrDefault().Email = @group.applicationuser.Email;
            tables.applicationusers.FirstOrDefault().Firstname = @group.applicationuser.Firstname;
            tables.applicationusers.FirstOrDefault().Lastname = @group.applicationuser.Lastname;

            return View(tables);

            /*return View(@group);*/
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, Group item, ApplicationUser user)
        {
            if (_context.Groups == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Groups'  is null.");
            }
            var @group = await _context.Groups.FindAsync(item.Id);
            if (@group != null)
            {
                _context.Groups.Remove(@group);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GroupExists(int id)
        {
          return (_context.Groups?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public IActionResult Addindex(int id)
        {

            var tables = new StudentViewModel
            {
                Groups = _context.Groups.ToList(),
                applicationusers = _context.applicationUsers.ToList(),
                students = _context.Students.ToList()
            };

            tables.Groups.FirstOrDefault().Id = id;

            return View(tables);
        }

        public IActionResult Add(int id)
        {

            var tables = new StudentViewModel
            {
                Groups = _context.Groups.ToList(),
                applicationusers = _context.applicationUsers.ToList(),
                students = _context.Students.ToList()
            };

            tables.Groups.FirstOrDefault().Id = id ;

            return View(tables);
        }

        // POST: Groups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Group item, ApplicationUser user, Student student)
        {

            foreach (var categ in _context.applicationUsers)
            {
                if (categ.Email == user.Email)
                {
                    student.applicationuser = categ;
                    break;
                }
            }

            foreach (var categ in _context.Groups)
            {
                if (categ.Id == item.Id)
                {
                    student.group = categ;
                    break;
                }
            }

            student.Id = null;

            _context.Students.Add(student);
            _context.SaveChanges();
            return RedirectToAction("Addindex", item.Id);

            /*return RedirectToAction(nameof(Index));*/
        }


        public async Task<IActionResult> Deletestudent(string? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var @student = await _context.Students
                .FirstOrDefaultAsync(m => m.applicationuser.Id == id);
            if (@student == null)
            {
                return NotFound();
            }

            var tables = new StudentViewModel
            {
                Groups = _context.Groups.ToList(),
                applicationusers = _context.applicationUsers.ToList(),
                students = _context.Students.ToList()
            };

            tables.Groups.FirstOrDefault().Id = @student.group.Id;
            tables.Groups.FirstOrDefault().Name = @student.group.Name;
            tables.Groups.FirstOrDefault().Description = @student.group.Description;
            tables.Groups.FirstOrDefault().applicationuser = @student.applicationuser;
            tables.applicationusers.FirstOrDefault().Email = @student.applicationuser.Email;
            tables.applicationusers.FirstOrDefault().Firstname = @student.applicationuser.Firstname;
            tables.applicationusers.FirstOrDefault().Lastname = @student.applicationuser.Lastname;

            return View(tables);

            /*return View(@group);*/
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Deletestudent")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletestudentConfirmed(Student student, int id, Group item, ApplicationUser user)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Groups'  is null.");
            }
            var @studentb = await _context.Students.FindAsync(student.Id);
            if (@studentb != null)
            {
                _context.Students.Remove(@studentb);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Addindex", @studentb.group.Id);
        }
    }
}
