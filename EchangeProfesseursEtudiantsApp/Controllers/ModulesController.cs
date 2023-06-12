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
using System.Reflection;
using System.Text.RegularExpressions;

namespace EchangeProfesseursEtudiantsApp.Controllers
{
    [Authorize(Roles = "Teacher")]
    public class ModulesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ModulesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Modules
        public async Task<IActionResult> Index(int? id)
        {

            var tables = new ModuleViewModel
            {
                Groups = _context.Groups.ToList(),
                applicationusers = _context.applicationUsers.ToList(),
                modules = _context.Modules.ToList()
            };

            foreach(var iteam in tables.Groups)
            {
                if (iteam.Idgroup == id)
                {
                    tables.Groups.FirstOrDefault().Namegroup = iteam.Namegroup;
                    tables.Groups.FirstOrDefault().Idgroup = iteam.Idgroup;
                    break;
                }
            }

            return View(tables);

            /*return _context.Modules != null ? 
                          View(await _context.Modules.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Modules'  is null.");*/
        }

        // GET: Modules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Modules == null)
            {
                return NotFound();
            }

            var @module = await _context.Modules
                .FirstOrDefaultAsync(m => m.Idmodule == id);
            if (@module == null)
            {
                return NotFound();
            }

            var tables = new ModuleViewModel
            {
                Groups = _context.Groups.ToList(),
                applicationusers = _context.applicationUsers.ToList(),
                modules = _context.Modules.ToList()
            };

            tables.modules.FirstOrDefault().Idmodule = @module.Idmodule;
            tables.modules.FirstOrDefault().Namemodule = @module.Namemodule;
            tables.modules.FirstOrDefault().Descriptionmodule = @module.Descriptionmodule;
            tables.modules.FirstOrDefault().Coefficientmodule = @module.Coefficientmodule;
            tables.modules.FirstOrDefault().applicationusermodule.Firstname = @module.applicationusermodule.Firstname;
            tables.modules.FirstOrDefault().applicationusermodule.Lastname = @module.applicationusermodule.Lastname;
            tables.modules.FirstOrDefault().applicationusermodule.Email = @module.applicationusermodule.Email;

            return View(tables);
        }

        // GET: Modules/Create
        public IActionResult Create(int? id)
        {
            var tables = new ModuleViewModel
            {
                Groups = _context.Groups.ToList(),
                applicationusers = _context.applicationUsers.ToList(),
                modules = _context.Modules.ToList()
            };

            foreach (var iteam in tables.Groups)
            {
                if (iteam.Idgroup == id)
                {
                    tables.Groups.FirstOrDefault().Idgroup = iteam.Idgroup;
                    break;
                }
            }

            return View(tables);
        }

        // POST: Modules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Models.Group item, ApplicationUser user, Models.Module obj)
        {
            obj.Idmodule = null;
            foreach (var categ in _context.applicationUsers)
            {
                if (categ.Email == user.Email)
                {
                    obj.applicationusermodule = categ;
                    break;
                }
            }

            foreach (var categ in _context.Groups)
            {
                if (categ.Idgroup == item.Idgroup)
                {
                    obj.groupmodule = categ;
                    break;
                }
            }

            _context.Modules.Add(obj);
            _context.SaveChanges();
            return RedirectToAction("Index");

            /*if (ModelState.IsValid)
            {
                _context.Add(@module);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@module);*/
        }

        // GET: Modules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Modules == null)
            {
                return NotFound();
            }

            var @module = await _context.Modules.FindAsync(id);
            if (@module == null)
            {
                return NotFound();
            }

            var tables = new ModuleViewModel
            {
                Groups = _context.Groups.ToList(),
                applicationusers = _context.applicationUsers.ToList(),
                modules = _context.Modules.ToList()
            };

            foreach (var obj in tables.modules)
            {
                if (obj.Idmodule == id)
                {
                    tables.Groups.FirstOrDefault().Idgroup = obj.groupmodule.Idgroup;
                }
            }

            tables.modules.FirstOrDefault().Idmodule = id;
            tables.modules.FirstOrDefault().Namemodule = @module.Namemodule;
            tables.modules.FirstOrDefault().Coefficientmodule = @module.Coefficientmodule;
            tables.modules.FirstOrDefault().Descriptionmodule = @module.Descriptionmodule;
            tables.modules.FirstOrDefault().applicationusermodule.Email = @module.applicationusermodule.Email;

            return View(tables);
        }

        // POST: Modules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Models.Module obj, Models.Group item)
        {
            int i = 0;
            int j = 0;
            foreach (var categ in _context.applicationUsers)
            {
                if (obj.applicationusermodule.Email == categ.Email)
                {
                    obj.applicationusermodule = categ;
                    i = 1;
                    break;
                }
            }

            
            obj.Idmodule = id;

            foreach (var categ in _context.Groups)
            {
                if (item.Idgroup == categ.Idgroup)
                {
                    obj.groupmodule = categ;
                    j = 1;
                    break;
                }
            }

            /*foreach (var categ in _context.Groups)
            {
                if (obj.group.Id == item.Id)
                {
                    obj.group = item;
                    j=1;
                    break;
                }
            }*/

            if (i == 1 && j==1)
            {
                _context.Modules.Update(obj);
                _context.SaveChanges();
            }

            return RedirectToAction("Index", obj.groupmodule.Idgroup);

            /*
            if (id != @module.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@module);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModuleExists(@module.Id))
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
            return View(@module);*/
        }

        // GET: Modules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Modules == null)
            {
                return NotFound();
            }

            var @module = await _context.Modules
                .FirstOrDefaultAsync(m => m.Idmodule == id);
            if (@module == null)
            {
                return NotFound();
            }

            return View(@module);
        }

        // POST: Modules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Modules == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Modules'  is null.");
            }
            var @module = await _context.Modules.FindAsync(id);
            if (@module != null)
            {
                _context.Modules.Remove(@module);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModuleExists(int id)
        {
          return (_context.Modules?.Any(e => e.Idmodule == id)).GetValueOrDefault();
        }

        [Authorize(Roles = "Teacher")]
        public IActionResult AddIndex(int id)
        {

            var tables = new ElementViewModel
            {
                applicationusers = _context.applicationUsers.ToList(),
                modules = _context.Modules.ToList(),
                elements = _context.Elements.ToList()
            };

            tables.modules.FirstOrDefault().Idmodule = id;
            foreach (var obj in _context.Modules)
            {
                if (obj.Idmodule == id)
                {
                    tables.modules.FirstOrDefault().Namemodule = obj.Namemodule;
                }

            }


            return View(tables);
        }

        [Authorize(Roles = "Teacher")]
        public IActionResult Add(int id)
        {

            var tables = new ElementViewModel
            {
                applicationusers = _context.applicationUsers.ToList(),
                elements = _context.Elements.ToList(),
                modules = _context.Modules.ToList()
            };

            tables.modules.FirstOrDefault().Idmodule = id;
            foreach (var obj in _context.Modules)
            {
                if (obj.Idmodule == id)
                {
                    tables.modules.FirstOrDefault().Namemodule = obj.Namemodule;
                }

            }


            return View(tables);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Add(Models.Module item, ApplicationUser user, Element element)
        {

            foreach (var categ in _context.applicationUsers)
            {
                if (categ.Email == user.Email)
                {
                    element.applicationuserelement = categ;
                    break;
                }
            }

            foreach (var categ in _context.Modules)
            {
                if (categ.Idmodule == item.Idmodule)
                {
                    element.moduleelement = categ;
                    break;
                }
            }

            element.Idelement = null;

            _context.Elements.Add(element);
            _context.SaveChanges();
            return RedirectToAction("Addindex", item.Idmodule);

            /*return RedirectToAction(nameof(Index));*/
        }

    }
}
