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
    
    public class GroupsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GroupsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Groups
        [Authorize(Roles = "Administrator")]
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
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Groups == null)
            {
                return NotFound();
            }

            var @group = await _context.Groups
                .FirstOrDefaultAsync(m => m.Idgroup == id);
            if (@group == null)
            {
                return NotFound();
            }

            var tables = new GroupViewModel
            {
                Groups = _context.Groups.ToList(),
                applicationusers = _context.applicationUsers.ToList()
            };

            tables.Groups.FirstOrDefault().Idgroup = @group.Idgroup;
            tables.Groups.FirstOrDefault().Namegroup = @group.Namegroup;
            tables.Groups.FirstOrDefault().Descriptiongroup = @group.Descriptiongroup;
            tables.Groups.FirstOrDefault().applicationusergroup = @group.applicationusergroup;
            tables.applicationusers.FirstOrDefault().Email = @group.applicationusergroup.Email;
            tables.applicationusers.FirstOrDefault().Firstname = @group.applicationusergroup.Firstname;
            tables.applicationusers.FirstOrDefault().Lastname = @group.applicationusergroup.Lastname;

            return View(tables);

            /*return View(@group);*/
        }

        // GET: Groups/Create
        [Authorize(Roles = "Administrator")]
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
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create(Group item, ApplicationUser user)
        {

            foreach (var categ in _context.applicationUsers)
            {
                if (categ.Email == user.Email)
                {
                    item.applicationusergroup = categ;
                    break;
                }
            }

            _context.Groups.Add(item);
            _context.SaveChanges();
            return RedirectToAction("Index");

            /*return RedirectToAction(nameof(Index));*/
        }

        // GET: Groups/Edit/5
        [Authorize(Roles = "Administrator")]
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

            tables.Groups.FirstOrDefault().Idgroup = @group.Idgroup;
            tables.Groups.FirstOrDefault().Namegroup = @group.Namegroup;
            tables.Groups.FirstOrDefault().Descriptiongroup = @group.Descriptiongroup;
            tables.Groups.FirstOrDefault().applicationusergroup = @group.applicationusergroup;
            tables.applicationusers.FirstOrDefault().Email = @group.applicationusergroup.Email;

            return View(tables);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id, Group item, ApplicationUser user)
        {
            int i = 0;
            foreach (var categ in _context.applicationUsers)
            {
                if (item.applicationusergroup.Email ==categ.Email)
                {
                    item.applicationusergroup.Id = categ.Id;
                    item.applicationusergroup.Firstname = categ.Firstname;
                    item.applicationusergroup.Lastname = categ.Lastname;
                    item.applicationusergroup = categ;
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
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Groups == null)
            {
                return NotFound();
            }

            var @group = await _context.Groups
                .FirstOrDefaultAsync(m => m.Idgroup == id);
            if (@group == null)
            {
                return NotFound();
            }

            var tables = new GroupViewModel
            {
                Groups = _context.Groups.ToList(),
                applicationusers = _context.applicationUsers.ToList()
            };

            tables.Groups.FirstOrDefault().Idgroup = @group.Idgroup;
            tables.Groups.FirstOrDefault().Namegroup = @group.Namegroup;
            tables.Groups.FirstOrDefault().Descriptiongroup = @group.Descriptiongroup;
            tables.Groups.FirstOrDefault().applicationusergroup = @group.applicationusergroup;
            tables.applicationusers.FirstOrDefault().Email = @group.applicationusergroup.Email;
            tables.applicationusers.FirstOrDefault().Firstname = @group.applicationusergroup.Firstname;
            tables.applicationusers.FirstOrDefault().Lastname = @group.applicationusergroup.Lastname;

            return View(tables);

            /*return View(@group);*/
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(int id, Group item, ApplicationUser user)
        {
            if (_context.Groups == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Groups'  is null.");
            }
            var @group = await _context.Groups.FindAsync(item.Idgroup);
            if (@group != null)
            {
                _context.Groups.Remove(@group);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GroupExists(int id)
        {
          return (_context.Groups?.Any(e => e.Idgroup == id)).GetValueOrDefault();
        }

        [Authorize(Roles = "Administrator, Teacher")]
        public IActionResult Addindex(int id)
        {

            var tables = new StudentViewModel
            {
                Groups = _context.Groups.ToList(),
                applicationusers = _context.applicationUsers.ToList(),
                students = _context.Students.ToList()
            };

            tables.Groups.FirstOrDefault().Idgroup = id;

            return View(tables);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Add(int id)
        {

            var tables = new StudentViewModel
            {
                Groups = _context.Groups.ToList(),
                applicationusers = _context.applicationUsers.ToList(),
                students = _context.Students.ToList()
            };

            tables.Groups.FirstOrDefault().Idgroup = id ;
            foreach(var obj in _context.Groups)
            {
                if (obj.Idgroup == id)
                {
                    tables.Groups.FirstOrDefault().Namegroup = obj.Namegroup;
                }
                
            }
            

            return View(tables);
        }

        // POST: Groups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Add(Group item, ApplicationUser user, Student student)
        {

            foreach (var categ in _context.applicationUsers)
            {
                if (categ.Email == user.Email)
                {
                    student.applicationuserstudent = categ;
                    break;
                }
            }

            foreach (var categ in _context.Groups)
            {
                if (categ.Idgroup == item.Idgroup)
                {
                    student.groupstudent = categ;
                    break;
                }
            }

            student.Idstudent = null;

            _context.Students.Add(student);
            _context.SaveChanges();
            return RedirectToAction("Addindex", item.Idgroup);

            /*return RedirectToAction(nameof(Index));*/
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Deletestudent(string? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var @student = await _context.Students
                .FirstOrDefaultAsync(m => m.applicationuserstudent.Id == id);
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

            tables.Groups.FirstOrDefault().Idgroup = @student.groupstudent.Idgroup;
            tables.Groups.FirstOrDefault().Namegroup = @student.groupstudent.Namegroup;
            tables.Groups.FirstOrDefault().Descriptiongroup = @student.groupstudent.Descriptiongroup;
            tables.Groups.FirstOrDefault().applicationusergroup = @student.applicationuserstudent;
            tables.applicationusers.FirstOrDefault().Email = @student.applicationuserstudent.Email;
            tables.applicationusers.FirstOrDefault().Firstname = @student.applicationuserstudent.Firstname;
            tables.applicationusers.FirstOrDefault().Lastname = @student.applicationuserstudent.Lastname;

            return View(tables);

            /*return View(@group);*/
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Deletestudent")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeletestudentConfirmed(Student student, int id, Group item, ApplicationUser user)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Groups'  is null.");
            }
            var @studentb = await _context.Students.FindAsync(student.Idstudent);
            if (@studentb != null)
            {
                _context.Students.Remove(@studentb);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Addindex", @studentb.groupstudent.Idgroup);
        }

        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Indexteacher(string user)
        {

            var tables = new GroupViewModel
            {
                Groups = _context.Groups.ToList(),
                applicationusers = _context.applicationUsers.ToList()
            };

            tables.applicationusers.FirstOrDefault().Email = user;

            return View(tables);
            /*return _context.Groups != null ? 
                        View(await _context.Groups.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Groups'  is null.");*/
        }

        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Indexstudent(string user)
        {

            var tables = new StudentViewModel
            {
                Groups = _context.Groups.ToList(),
                applicationusers = _context.applicationUsers.ToList(),
                students = _context.Students.ToList()
            };

            tables.applicationusers.FirstOrDefault().Email = user;

            return View(tables);
            /*return _context.Groups != null ? 
                        View(await _context.Groups.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Groups'  is null.");*/
        }
    }
}
