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
    public class ResumesController : Controller
    {
        private readonly mscshubContext _context;

        public ResumesController(mscshubContext context)
        {
            _context = context;
        }

        // GET: Resumes
        public async Task<IActionResult> Index()
        {
            var mscshubContext = _context.Resumes.Include(r => r.ResumeNavigation).Include(r => r.Student);
            return View(await mscshubContext.ToListAsync());
        }

        // GET: Resumes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resume = await _context.Resumes
                .Include(r => r.ResumeNavigation)
                .Include(r => r.Student)
                .FirstOrDefaultAsync(m => m.ResumeId == id);
            if (resume == null)
            {
                return NotFound();
            }

            return View(resume);
        }

        // GET: Resumes/Create
        public IActionResult Create()
        {
            ViewData["ResumeId"] = new SelectList(_context.OnlineActivities, "ActivityId", "ActivityId");
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId");
            return View();
        }

        // POST: Resumes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ResumeId,StudentId,PostDate")] Resume resume)
        {
            if (ModelState.IsValid)
            {
                _context.Add(resume);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ResumeId"] = new SelectList(_context.OnlineActivities, "ActivityId", "ActivityId", resume.ResumeId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", resume.StudentId);
            return View(resume);
        }

        // GET: Resumes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resume = await _context.Resumes.FindAsync(id);
            if (resume == null)
            {
                return NotFound();
            }
            ViewData["ResumeId"] = new SelectList(_context.OnlineActivities, "ActivityId", "ActivityId", resume.ResumeId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", resume.StudentId);
            return View(resume);
        }

        // POST: Resumes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ResumeId,StudentId,PostDate")] Resume resume)
        {
            if (id != resume.ResumeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(resume);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResumeExists(resume.ResumeId))
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
            ViewData["ResumeId"] = new SelectList(_context.OnlineActivities, "ActivityId", "ActivityId", resume.ResumeId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", resume.StudentId);
            return View(resume);
        }

        // GET: Resumes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resume = await _context.Resumes
                .Include(r => r.ResumeNavigation)
                .Include(r => r.Student)
                .FirstOrDefaultAsync(m => m.ResumeId == id);
            if (resume == null)
            {
                return NotFound();
            }

            return View(resume);
        }

        // POST: Resumes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var resume = await _context.Resumes.FindAsync(id);
            _context.Resumes.Remove(resume);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResumeExists(string id)
        {
            return _context.Resumes.Any(e => e.ResumeId == id);
        }
    }
}
