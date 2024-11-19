using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lexicon_garage3.Core.Entities;
using lexicon_garage3.Web.Data;

namespace lexicon_garage3.Web.Controllers
{
    public class ParkingSpotsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ParkingSpotsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ParkingSpots
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ParkingSpot.Include(p => p.Vehicle);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ParkingSpots/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkingSpot = await _context.ParkingSpot
                .Include(p => p.Vehicle)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parkingSpot == null)
            {
                return NotFound();
            }

            return View(parkingSpot);
        }

        // GET: ParkingSpots/Create
        public IActionResult Create()
        {
            ViewData["RegNumber"] = new SelectList(_context.Set<Vehicle>(), "RegNumber", "RegNumber");
            return View();
        }

        // POST: ParkingSpots/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Size,ParkingNumber,IsAvailable,HourRate,RegNumber")] ParkingSpot parkingSpot)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parkingSpot);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RegNumber"] = new SelectList(_context.Set<Vehicle>(), "RegNumber", "RegNumber", parkingSpot.RegNumber);
            return View(parkingSpot);
        }

        // GET: ParkingSpots/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkingSpot = await _context.ParkingSpot.FindAsync(id);
            if (parkingSpot == null)
            {
                return NotFound();
            }
            ViewData["RegNumber"] = new SelectList(_context.Set<Vehicle>(), "RegNumber", "RegNumber", parkingSpot.RegNumber);
            return View(parkingSpot);
        }

        // POST: ParkingSpots/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Size,ParkingNumber,IsAvailable,HourRate,RegNumber")] ParkingSpot parkingSpot)
        {
            if (id != parkingSpot.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parkingSpot);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParkingSpotExists(parkingSpot.Id))
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
            ViewData["RegNumber"] = new SelectList(_context.Set<Vehicle>(), "RegNumber", "RegNumber", parkingSpot.RegNumber);
            return View(parkingSpot);
        }

        // GET: ParkingSpots/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkingSpot = await _context.ParkingSpot
                .Include(p => p.Vehicle)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parkingSpot == null)
            {
                return NotFound();
            }

            return View(parkingSpot);
        }

        // POST: ParkingSpots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var parkingSpot = await _context.ParkingSpot.FindAsync(id);
            if (parkingSpot != null)
            {
                _context.ParkingSpot.Remove(parkingSpot);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParkingSpotExists(string id)
        {
            return _context.ParkingSpot.Any(e => e.Id == id);
        }
    }
}
