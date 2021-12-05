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
    public class SportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Make the index view available to everyone
        // GET: Sports
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View("Index", await _context.Sports.ToListAsync());
        }

        //Make the details view public
        // GET: Sports/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return View("404");
            }

            var sport = await _context.Sports
                .FirstOrDefaultAsync(m => m.SportId == id);
            if (sport == null)
            {
                return View("404");
            }

            return View("Details", sport);
        }

        // GET: Sports/Create
        public IActionResult Create()
        {
            return View("Create");
        }

        // POST: Sports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SportId,SportName,NumOfPlayers,NumOfTeams,Equipment")] Sport sport)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("Create", sport);
        }

        // GET: Sports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return View("404");
            }

            var sport = await _context.Sports.FindAsync(id);
            if (sport == null)
            {
                return View("404");
            }
            return View("Edit", sport);
        }

        // POST: Sports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SportId,SportName,NumOfPlayers,NumOfTeams,Equipment")] Sport sport)
        {
            if (id != sport.SportId)
            {
                return View("404");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SportExists(sport.SportId))
                    {
                        return View("404");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View("Edit", sport);
        }

        // GET: Sports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return View("404");
            }

            var sport = await _context.Sports
                .FirstOrDefaultAsync(m => m.SportId == id);
            if (sport == null)
            {
                return View("404");
            }

            return View("Delete", sport);
        }

        // POST: Sports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sport = await _context.Sports.FindAsync(id);
            _context.Sports.Remove(sport);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SportExists(int id)
        {
            return _context.Sports.Any(e => e.SportId == id);
        }
    }
}
