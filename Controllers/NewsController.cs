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
    public class NewsController : Controller
    {
        private readonly mscshubContext _context;

        public NewsController(mscshubContext context)
        {
            _context = context;
        }

        // GET: News
        public async Task<IActionResult> Index()
        {
            var mscshubContext = _context.News.Include(n => n.Lecturer).Include(n => n.NewsNavigation).Include(n => n.Student);
            return View(await mscshubContext.ToListAsync());
        }

        // GET: News/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .Include(n => n.Lecturer)
                .Include(n => n.NewsNavigation)
                .Include(n => n.Student)
                .FirstOrDefaultAsync(m => m.NewsId == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // GET: News/Create
        public IActionResult Create()
        {
            ViewData["LecturerId"] = new SelectList(_context.Lecturers, "LecturerId", "LecturerId");
            ViewData["NewsId"] = new SelectList(_context.OnlineActivities, "ActivityId", "ActivityId");
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId");
            return View();
        }

        // POST: News/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NewsId,PostDate,StudentId,LecturerId")] News news)
        {
            if (ModelState.IsValid)
            {
                _context.Add(news);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LecturerId"] = new SelectList(_context.Lecturers, "LecturerId", "LecturerId", news.LecturerId);
            ViewData["NewsId"] = new SelectList(_context.OnlineActivities, "ActivityId", "ActivityId", news.NewsId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", news.StudentId);
            return View(news);
        }

        // GET: News/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News.FindAsync(id);
            if (news == null)
            {
                return NotFound();
            }
            ViewData["LecturerId"] = new SelectList(_context.Lecturers, "LecturerId", "LecturerId", news.LecturerId);
            ViewData["NewsId"] = new SelectList(_context.OnlineActivities, "ActivityId", "ActivityId", news.NewsId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", news.StudentId);
            return View(news);
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("NewsId,PostDate,StudentId,LecturerId")] News news)
        {
            if (id != news.NewsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(news);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsExists(news.NewsId))
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
            ViewData["LecturerId"] = new SelectList(_context.Lecturers, "LecturerId", "LecturerId", news.LecturerId);
            ViewData["NewsId"] = new SelectList(_context.OnlineActivities, "ActivityId", "ActivityId", news.NewsId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", news.StudentId);
            return View(news);
        }

        // GET: News/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .Include(n => n.Lecturer)
                .Include(n => n.NewsNavigation)
                .Include(n => n.Student)
                .FirstOrDefaultAsync(m => m.NewsId == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var news = await _context.News.FindAsync(id);
            _context.News.Remove(news);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsExists(string id)
        {
            return _context.News.Any(e => e.NewsId == id);
        }
    }
}
