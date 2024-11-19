using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lexicon_garage3.Core.Entities;
using lexicon_garage3.Persistance.Data;
using lexicon_garage3.Web.Models.ViewModels.ParkingSpotsViewModels;
using System.Reflection.Emit;


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
            var parkingSpots = await _context.ParkingSpot
          .Include(p => p.Vehicle)
          .ToListAsync();

            var viewModels = parkingSpots.Select(p => new IndexParkingSpotViewModel
            {
                Id = p.Id,
                Size = p.Size,
                ParkingNumber = p.ParkingNumber,
                IsAvailable = p.IsAvailable,
                HourRate = p.HourRate,
            }).ToList();

            return View(viewModels);
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
            ViewData["Size"] = new SelectList(
             Enum.GetValues(typeof(Size))
            .Cast<Size>()
            .Select(s => new { Value = (int)s, Text = s.ToString() }),
        "Value",
        "Text"
    );
            return View();
        }

        // POST: ParkingSpots/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateParkingSpotsViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var parkingSpot = new ParkingSpot
                    {
                        Id = Guid.NewGuid().ToString(),
                        Size = model.Size.ToString(),
                        ParkingNumber = model.ParkingNumber,
                        IsAvailable = true,
                        HourRate = model.HourRate
                    };

                    _context.Add(parkingSpot);
                    await _context.SaveChangesAsync();

               
                    TempData["SuccessMessage"] = "Parking spot created successfully!";
                }
                catch (Exception)
                {
                    
                    TempData["ErrorMessage"] = "An error occurred while creating the parking spot.";
                    return View(model);
                }
            }
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                TempData["ErrorMessage"] = "Validation Errors: " + string.Join(", ", errors);
            }

            return View(model); // not passed validation
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
