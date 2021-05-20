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
    public class TutorialPostsController : Controller
    {
        private readonly mscshubContext _context;

        public TutorialPostsController(mscshubContext context)
        {
            _context = context;
        }

        // GET: TutorialPosts
        public async Task<IActionResult> Index()
        {
            var mscshubContext = _context.TutorialPosts.Include(t => t.Lecturer);
            return View(await mscshubContext.ToListAsync());
        }

        // GET: TutorialPosts/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tutorialPost = await _context.TutorialPosts
                .Include(t => t.Lecturer)
                .FirstOrDefaultAsync(m => m.TutorialId == id);
            if (tutorialPost == null)
            {
                return NotFound();
            }

            return View(tutorialPost);
        }

        // GET: TutorialPosts/Create
        public IActionResult Create()
        {
            ViewData["LecturerId"] = new SelectList(_context.Lecturers, "LecturerId", "LecturerId");
            return View();
        }

        // POST: TutorialPosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TutorialId,CourseNo,PostDate,LecturerId,Url")] TutorialPost tutorialPost)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tutorialPost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LecturerId"] = new SelectList(_context.Lecturers, "LecturerId", "LecturerId", tutorialPost.LecturerId);
            return View(tutorialPost);
        }

        // GET: TutorialPosts/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tutorialPost = await _context.TutorialPosts.FindAsync(id);
            if (tutorialPost == null)
            {
                return NotFound();
            }
            ViewData["LecturerId"] = new SelectList(_context.Lecturers, "LecturerId", "LecturerId", tutorialPost.LecturerId);
            return View(tutorialPost);
        }

        // POST: TutorialPosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("TutorialId,CourseNo,PostDate,LecturerId,Url")] TutorialPost tutorialPost)
        {
            if (id != tutorialPost.TutorialId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tutorialPost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TutorialPostExists(tutorialPost.TutorialId))
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
            ViewData["LecturerId"] = new SelectList(_context.Lecturers, "LecturerId", "LecturerId", tutorialPost.LecturerId);
            return View(tutorialPost);
        }

        // GET: TutorialPosts/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tutorialPost = await _context.TutorialPosts
                .Include(t => t.Lecturer)
                .FirstOrDefaultAsync(m => m.TutorialId == id);
            if (tutorialPost == null)
            {
                return NotFound();
            }

            return View(tutorialPost);
        }

        // POST: TutorialPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var tutorialPost = await _context.TutorialPosts.FindAsync(id);
            _context.TutorialPosts.Remove(tutorialPost);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TutorialPostExists(string id)
        {
            return _context.TutorialPosts.Any(e => e.TutorialId == id);
        }
    }
}
