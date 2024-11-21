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
        public async Task<IActionResult> IndexParkingPlace()
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

          


            return View("Index", viewModels);
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
            return View(new CreateParkingSpotsViewModel());
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

            var viewModel = new EditParkingSpotViewModel
            {
                Id = parkingSpot.Id,
                Size = Enum.TryParse<Size>(parkingSpot.Size, out var size) ? size : Size.Small,
                ParkingNumber = parkingSpot.ParkingNumber,
                HourRate = parkingSpot.HourRate,
                IsAvailable = parkingSpot.IsAvailable,
                RegNumber = parkingSpot.RegNumber
            };

            ViewData["RegNumber"] = new SelectList(_context.Set<Vehicle>(), "RegNumber", "RegNumber", parkingSpot.RegNumber);


            return View(viewModel);
        }

        // POST: ParkingSpots/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, EditParkingSpotViewModel model)
        {
            if (id != model.Id)
            {
                TempData["ErrorMessage"] = "Invalid parking spot ID.";
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var parkingSpot = await _context.ParkingSpot.FindAsync(id);
                    if (parkingSpot == null)
                    {
                        TempData["ErrorMessage"] = "Parking spot not found!";
                        return RedirectToAction(nameof(Index));
                    }

                    parkingSpot.Size = model.Size.ToString();
                    parkingSpot.ParkingNumber = model.ParkingNumber;
                    parkingSpot.HourRate = model.HourRate;
                    parkingSpot.IsAvailable = model.IsAvailable;
                    parkingSpot.RegNumber = model.RegNumber;

                    _context.Update(parkingSpot);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Parking spot updated successfully!";
                }
                catch (Exception)
                {
                    TempData["ErrorMessage"] = "An error occurred while updating the parking spot.";
                    return View(model);
                }
            }
            return View(model); // in case not passing validation
        }


        // GET: ParkingSpots/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = "Parking spot not found!";
                return NotFound();
            }

            var parkingSpot = await _context.ParkingSpot
                .Include(p => p.Vehicle)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parkingSpot == null)
            {
                TempData["ErrorMessage"] = "Parking spot not found!";
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
            TempData["SuccessMessage"] = "Parking spot delete successfully!";
            return RedirectToAction(nameof(Index));
        }

        private bool ParkingSpotExists(string id)
        {
            return _context.ParkingSpot.Any(e => e.Id == id);
        }

        // POST: ParkingSpots/BookingToggle/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BookingToggle(string id)
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
            parkingSpot.IsAvailable = !parkingSpot.IsAvailable;
            try
            {
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Parking spot availability updated successfully.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while updating the parking spot availability.";
            }
            return RedirectToAction(nameof(IndexParkingPlace));
        }

        // GET: ParkingSpots/Statistic/5
        public async Task<IActionResult> Statistic()
        {
            var parkingSpots = await _context.ParkingSpot.ToListAsync();

            var totalSpots = parkingSpots.Count;
            var availableSpots = parkingSpots.Count(ps => ps.IsAvailable);
            var occupiedSpots = totalSpots - availableSpots;

            var model = new ParkingStatisticsViewModel
            {
                TotalSpots = totalSpots,
                AvailableSpots = availableSpots,
                OccupiedSpots = occupiedSpots
            };

            return View(model);
        }


        public async Task<IActionResult> Search(SearchParkingSpotViewModel viewModel)
        {
            var parkingSpots = _context.ParkingSpot.AsQueryable();

            if (!string.IsNullOrWhiteSpace(viewModel.Size))
            {
                parkingSpots = parkingSpots.Where(m => m.Size.Contains(viewModel.Size));
            }

            if (viewModel.ParkingNumber > 0)
            {
                parkingSpots = parkingSpots.Where(m => m.ParkingNumber == viewModel.ParkingNumber);
            }
;

            var viewModels = parkingSpots.Select(p => new IndexParkingSpotViewModel
            {
                Id = p.Id,
                Size = p.Size,
                ParkingNumber = p.ParkingNumber,
                IsAvailable = p.IsAvailable,
                HourRate = p.HourRate,
            }).ToList();
            
            return View("Index", viewModels);
        }

    }
}
