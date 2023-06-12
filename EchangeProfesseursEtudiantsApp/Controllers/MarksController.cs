using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EchangeProfesseursEtudiantsApp.Data;
using EchangeProfesseursEtudiantsApp.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using EchangeProfesseursEtudiantsApp.ViewModel;

namespace EchangeProfesseursEtudiantsApp.Controllers
{
    [Authorize(Roles = "Teacher")]
    public class MarksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MarksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Marks
        public async Task<IActionResult> Index(string? email, int? ide)
        {

            var tables = new MarkViewModel
            {
                Groups = _context.Groups.ToList(),
                applicationusers = _context.applicationUsers.ToList(),
                modules = _context.Modules.ToList(),
                marks = _context.Marks.ToList(),
                elements = _context.Elements.ToList(),
            };

            tables.elements.FirstOrDefault().Idelement = ide;
            tables.applicationusers.FirstOrDefault().Email = email;

            foreach (var item in _context.Elements)
            {
                if (item.Idelement == ide)
                {
                    tables.elements.FirstOrDefault().Nameelement = item.Nameelement;
                }
            }

            foreach (var item in _context.applicationUsers)
            {
                if (item.Email == email)
                {
                    tables.applicationusers.FirstOrDefault().Firstname = item.Firstname;
                    tables.applicationusers.FirstOrDefault().Lastname = item.Lastname;
                }
            }

            return View(tables);

            /*return _context.Marks != null ? 
                          View(await _context.Marks.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Marks'  is null.");*/
        }

        // GET: Marks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Marks == null)
            {
                return NotFound();
            }

            var mark = await _context.Marks
                .FirstOrDefaultAsync(m => m.Idmark == id);
            if (mark == null)
            {
                return NotFound();
            }

            return View(mark);
        }

        // GET: Marks/Create
        public IActionResult Create(string? email, int? ide)
        {
            var tables = new MarkViewModel
            {
                Groups = _context.Groups.ToList(),
                applicationusers = _context.applicationUsers.ToList(),
                modules = _context.Modules.ToList(),
                marks = _context.Marks.ToList(),
                elements = _context.Elements.ToList(),
            };

            tables.elements.FirstOrDefault().Idelement = ide;
            tables.applicationusers.FirstOrDefault().Email = email;

            foreach (var item in _context.Elements)
            {
                if (item.Idelement == ide)
                {
                    tables.elements.FirstOrDefault().Nameelement = item.Nameelement;
                }
            }

            foreach (var item in _context.applicationUsers)
            {
                if (item.Email == email)
                {
                    tables.applicationusers.FirstOrDefault().Firstname = item.Firstname;
                    tables.applicationusers.FirstOrDefault().Lastname = item.Lastname;
                }
            }
            return View(tables);
        }

        // POST: Marks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ApplicationUser user, Element element, Mark mark)
        {

            foreach (var obj in _context.applicationUsers)
            {
                if (obj.Email == user.Email)
                {
                    mark.applicationusermark = obj;
                }
            }

            foreach (var obj in _context.Elements)
            {
                if (obj.Idelement == element.Idelement)
                {
                    mark.elementmark = obj;
                }
            }

            mark.Idmark = null;

            _context.Add(mark);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Marks/Edit/5
        public async Task<IActionResult> Edit(string? email, int? ide, int? idm)
        {
            var tables = new MarkViewModel
            {
                Groups = _context.Groups.ToList(),
                applicationusers = _context.applicationUsers.ToList(),
                modules = _context.Modules.ToList(),
                marks = _context.Marks.ToList(),
                elements = _context.Elements.ToList(),
            };

            tables.elements.FirstOrDefault().Idelement = ide;
            tables.applicationusers.FirstOrDefault().Email = email;

            foreach (var item in _context.Elements)
            {
                if (item.Idelement == ide)
                {
                    tables.elements.FirstOrDefault().Nameelement = item.Nameelement;
                }
            }

            foreach (var item in _context.applicationUsers)
            {
                if (item.Email == email)
                {
                    tables.applicationusers.FirstOrDefault().Firstname = item.Firstname;
                    tables.applicationusers.FirstOrDefault().Lastname = item.Lastname;
                }
            }

            tables.marks.FirstOrDefault().Idmark = idm;
            return View(tables);
        }

        // POST: Marks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ApplicationUser user, Element element, Mark mark)
        {
            foreach (var obj in _context.applicationUsers)
            {
                if (obj.Email == user.Email)
                {
                    mark.applicationusermark = obj;
                }
            }

            foreach (var obj in _context.Elements)
            {
                if (obj.Idelement == element.Idelement)
                {
                    mark.elementmark = obj;
                }
            }

            _context.Marks.Update(mark);
            _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Marks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Marks == null)
            {
                return NotFound();
            }

            var mark = await _context.Marks
                .FirstOrDefaultAsync(m => m.Idmark == id);
            if (mark == null)
            {
                return NotFound();
            }

            return View(mark);
        }

        // POST: Marks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (_context.Marks == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Marks'  is null.");
            }
            var mark = await _context.Marks.FindAsync(id);
            if (mark != null)
            {
                _context.Marks.Remove(mark);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MarkExists(int? id)
        {
          return (_context.Marks?.Any(e => e.Idmark == id)).GetValueOrDefault();
        }
    }
}
