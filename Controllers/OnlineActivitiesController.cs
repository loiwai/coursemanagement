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
    public class OnlineActivitiesController : Controller
    {
        private readonly mscshubContext _context;

        public OnlineActivitiesController(mscshubContext context)
        {
            _context = context;
        }

        // GET: OnlineActivities
        public async Task<IActionResult> Index()
        {
            return View(await _context.OnlineActivities.ToListAsync());
        }

        // GET: OnlineActivities/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var onlineActivity = await _context.OnlineActivities
                .FirstOrDefaultAsync(m => m.ActivityId == id);
            if (onlineActivity == null)
            {
                return NotFound();
            }

            return View(onlineActivity);
        }

        // GET: OnlineActivities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OnlineActivities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ActivityId,ActivityName,Url,Activity")] OnlineActivity onlineActivity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(onlineActivity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(onlineActivity);
        }

        // GET: OnlineActivities/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var onlineActivity = await _context.OnlineActivities.FindAsync(id);
            if (onlineActivity == null)
            {
                return NotFound();
            }
            return View(onlineActivity);
        }

        // POST: OnlineActivities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ActivityId,ActivityName,Url,Activity")] OnlineActivity onlineActivity)
        {
            if (id != onlineActivity.ActivityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(onlineActivity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OnlineActivityExists(onlineActivity.ActivityId))
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
            return View(onlineActivity);
        }

        // GET: OnlineActivities/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var onlineActivity = await _context.OnlineActivities
                .FirstOrDefaultAsync(m => m.ActivityId == id);
            if (onlineActivity == null)
            {
                return NotFound();
            }

            return View(onlineActivity);
        }

        // POST: OnlineActivities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var onlineActivity = await _context.OnlineActivities.FindAsync(id);
            _context.OnlineActivities.Remove(onlineActivity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OnlineActivityExists(string id)
        {
            return _context.OnlineActivities.Any(e => e.ActivityId == id);
        }
    }
}
