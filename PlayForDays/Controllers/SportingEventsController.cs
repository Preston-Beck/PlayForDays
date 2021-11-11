using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlayForDays.Data;
using PlayForDays.Models;

namespace PlayForDays.Controllers
{
    //Private controller for admins only
    [Authorize(Roles = "Administrator")]
    public class SportingEventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SportingEventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Make the index view available to everyone
        // GET: SportingEvents
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SportingEvents.Include(s => s.Sport);
            return View(await applicationDbContext.ToListAsync());
        }

        //Make the details view public
        // GET: SportingEvents/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sportingEvent = await _context.SportingEvents
                .Include(s => s.Sport)
                .FirstOrDefaultAsync(m => m.SportingEventId == id);
            if (sportingEvent == null)
            {
                return NotFound();
            }

            return View(sportingEvent);
        }

        // GET: SportingEvents/Create
        public IActionResult Create()
        {
            ViewData["SportId"] = new SelectList(_context.Sports, "SportId", "SportName");
            return View();
        }

        // POST: SportingEvents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SportingEventId,Name,StartTime,EndTime,Address,City,Province,SportId")] SportingEvent sportingEvent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sportingEvent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SportId"] = new SelectList(_context.Sports, "SportId", "SportName", sportingEvent.SportId);
            return View(sportingEvent);
        }

        // GET: SportingEvents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sportingEvent = await _context.SportingEvents.FindAsync(id);
            if (sportingEvent == null)
            {
                return NotFound();
            }
            ViewData["SportId"] = new SelectList(_context.Sports, "SportId", "SportName", sportingEvent.SportId);
            return View(sportingEvent);
        }

        // POST: SportingEvents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SportingEventId,Name,StartTime,EndTime,Address,City,Province,SportId")] SportingEvent sportingEvent)
        {
            if (id != sportingEvent.SportingEventId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sportingEvent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SportingEventExists(sportingEvent.SportingEventId))
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
            ViewData["SportId"] = new SelectList(_context.Sports, "SportId", "SportName", sportingEvent.SportId);
            return View(sportingEvent);
        }

        // GET: SportingEvents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sportingEvent = await _context.SportingEvents
                .Include(s => s.Sport)
                .FirstOrDefaultAsync(m => m.SportingEventId == id);
            if (sportingEvent == null)
            {
                return NotFound();
            }

            return View(sportingEvent);
        }

        // POST: SportingEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sportingEvent = await _context.SportingEvents.FindAsync(id);
            _context.SportingEvents.Remove(sportingEvent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SportingEventExists(int id)
        {
            return _context.SportingEvents.Any(e => e.SportingEventId == id);
        }
    }
}
