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
    public class TranscriptsController : Controller
    {
        private readonly mscshubContext _context;

        public TranscriptsController(mscshubContext context)
        {
            _context = context;
        }

        // GET: Transcripts
        public async Task<IActionResult> Index()
        {
            var mscshubContext = _context.Transcripts.Include(t => t.Student).Include(t => t.TranscriptNavigation);
            return View(await mscshubContext.ToListAsync());
        }

        // GET: Transcripts/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transcript = await _context.Transcripts
                .Include(t => t.Student)
                .Include(t => t.TranscriptNavigation)
                .FirstOrDefaultAsync(m => m.TranscriptId == id);
            if (transcript == null)
            {
                return NotFound();
            }

            return View(transcript);
        }

        // GET: Transcripts/Create
        public IActionResult Create()
        {
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId");
            ViewData["TranscriptId"] = new SelectList(_context.OnlineActivities, "ActivityId", "ActivityId");
            return View();
        }

        // POST: Transcripts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TranscriptId,PostDate,StudentId")] Transcript transcript)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transcript);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", transcript.StudentId);
            ViewData["TranscriptId"] = new SelectList(_context.OnlineActivities, "ActivityId", "ActivityId", transcript.TranscriptId);
            return View(transcript);
        }

        // GET: Transcripts/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transcript = await _context.Transcripts.FindAsync(id);
            if (transcript == null)
            {
                return NotFound();
            }
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", transcript.StudentId);
            ViewData["TranscriptId"] = new SelectList(_context.OnlineActivities, "ActivityId", "ActivityId", transcript.TranscriptId);
            return View(transcript);
        }

        // POST: Transcripts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("TranscriptId,PostDate,StudentId")] Transcript transcript)
        {
            if (id != transcript.TranscriptId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transcript);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TranscriptExists(transcript.TranscriptId))
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
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", transcript.StudentId);
            ViewData["TranscriptId"] = new SelectList(_context.OnlineActivities, "ActivityId", "ActivityId", transcript.TranscriptId);
            return View(transcript);
        }

        // GET: Transcripts/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transcript = await _context.Transcripts
                .Include(t => t.Student)
                .Include(t => t.TranscriptNavigation)
                .FirstOrDefaultAsync(m => m.TranscriptId == id);
            if (transcript == null)
            {
                return NotFound();
            }

            return View(transcript);
        }

        // POST: Transcripts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var transcript = await _context.Transcripts.FindAsync(id);
            _context.Transcripts.Remove(transcript);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TranscriptExists(string id)
        {
            return _context.Transcripts.Any(e => e.TranscriptId == id);
        }
    }
}
