using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mscs.Models;

namespace mscs.Controllers
{
    public class CouresRegistrationsController : Controller
    {
        private readonly mscshubContext _context;

        public CouresRegistrationsController(mscshubContext context)
        {
            _context = context;
        }

        // GET: CouresRegistrations
        public async Task<IActionResult> Index()
        {
            var mscshubContext = _context.CouresRegistrations.Include(c => c.CourseNoNavigation).Include(c => c.Student);
            return View(await mscshubContext.ToListAsync());
        }

        // GET: CouresRegistrations/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var couresRegistration = await _context.CouresRegistrations
                .Include(c => c.CourseNoNavigation)
                .Include(c => c.Student)
                .FirstOrDefaultAsync(m => m.CourseRegNo == id);
            if (couresRegistration == null)
            {
                return NotFound();
            }

            return View(couresRegistration);
        }

        // GET: CouresRegistrations/Create
        public IActionResult Create()
        {
            ViewData["CourseNo"] = new SelectList(_context.Courses, "CourseNo", "CourseNo");
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId");
            return View();
        }

        // POST: CouresRegistrations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseRegNo,StudentId,CourseNo,RegDate")] CouresRegistration couresRegistration)
        {
            if (ModelState.IsValid)
            {
                _context.Add(couresRegistration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseNo"] = new SelectList(_context.Courses, "CourseNo", "CourseNo", couresRegistration.CourseNo);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", couresRegistration.StudentId);
            return View(couresRegistration);
        }

        // GET: CouresRegistrations/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var couresRegistration = await _context.CouresRegistrations.FindAsync(id);
            if (couresRegistration == null)
            {
                return NotFound();
            }
            ViewData["CourseNo"] = new SelectList(_context.Courses, "CourseNo", "CourseNo", couresRegistration.CourseNo);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", couresRegistration.StudentId);
            return View(couresRegistration);
        }

        // POST: CouresRegistrations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CourseRegNo,StudentId,CourseNo,RegDate")] CouresRegistration couresRegistration)
        {
            if (id != couresRegistration.CourseRegNo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(couresRegistration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CouresRegistrationExists(couresRegistration.CourseRegNo))
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
            ViewData["CourseNo"] = new SelectList(_context.Courses, "CourseNo", "CourseNo", couresRegistration.CourseNo);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", couresRegistration.StudentId);
            return View(couresRegistration);
        }

        // GET: CouresRegistrations/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var couresRegistration = await _context.CouresRegistrations
                .Include(c => c.CourseNoNavigation)
                .Include(c => c.Student)
                .FirstOrDefaultAsync(m => m.CourseRegNo == id);
            if (couresRegistration == null)
            {
                return NotFound();
            }

            return View(couresRegistration);
        }

        // POST: CouresRegistrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var couresRegistration = await _context.CouresRegistrations.FindAsync(id);
            _context.CouresRegistrations.Remove(couresRegistration);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CouresRegistrationExists(string id)
        {
            return _context.CouresRegistrations.Any(e => e.CourseRegNo == id);
        }
    }
}
