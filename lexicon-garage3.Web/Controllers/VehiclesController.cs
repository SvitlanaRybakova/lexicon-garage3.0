using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lexicon_garage3.Core.Entities;
using lexicon_garage3.Persistance.Data;
using lexicon_garage3.Web.Models.ViewModels.VehicleViewModels;

namespace lexicon_garage3.Web.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VehiclesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Vehicles
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Vehicle.Include(v => v.VehicleType);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle
                .Include(v => v.VehicleType)
                .FirstOrDefaultAsync(m => m.RegNumber == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // GET: Vehicles/Create
        public IActionResult Create()
        {
            ViewData["VehicleTypeId"] = new SelectList(_context.Set<VehicleType>(), "Id", "VehicleSize");
            var parkingSpots = _context.Set<ParkingSpot>().Where(p => p.IsAvailable);
            ViewData["ParkingSpotId"] = new SelectList(parkingSpots,"Id", "ParkingNumber");
            return View(new CreateVehicleViewModel());
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateVehicleViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var vehicleType = await _context.VehicleType
                    .FirstOrDefaultAsync(m => m.Id == viewModel.VehicleTypeId);

                var vehicle = new Vehicle
                {
                    RegNumber = viewModel.RegNumber,
                    Color = viewModel.Color,
                    Brand = viewModel.Brand,
                    Model = viewModel.Model,
                    ArrivalTime = DateTime.Now,
                    VehicleTypeId = viewModel.VehicleTypeId,
                    VehicleType = vehicleType
                };
                _context.Add(vehicle);

                var member = await _context.Member.FirstAsync(m => m.Id == "1");//TODO:change "1" to logged in user id
                member.Vehicles.Add(vehicle);

                var parkingSpot = await _context.ParkingSpot.FirstAsync(p => p.Id == viewModel.ParkingSpotId);
                parkingSpot.RegNumber = viewModel.RegNumber;
                parkingSpot.IsAvailable = false;
                parkingSpot.Vehicle = vehicle;

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["VehicleTypeId"] =
                new SelectList(_context.Set<VehicleType>(), "Id", "VehicleSize", viewModel.VehicleTypeId);
            return View(viewModel);
        }

        // GET: Vehicles/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            ViewData["VehicleTypeId"] = new SelectList(_context.Set<VehicleType>(), "Id", "VehicleSize", vehicle.VehicleTypeId);
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("RegNumber,Color,Brand,Model,ArrivalTime,CheckoutTime,VehicleTypeId")] Vehicle vehicle)
        {
            if (id != vehicle.RegNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicle.RegNumber))
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
            ViewData["VehicleTypeId"] = new SelectList(_context.Set<VehicleType>(), "Id", "VehicleSize", vehicle.VehicleTypeId);
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle
                .Include(v => v.VehicleType)
                .FirstOrDefaultAsync(m => m.RegNumber == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var vehicle = await _context.Vehicle.FindAsync(id);
            if (vehicle != null)
            {
                _context.Vehicle.Remove(vehicle);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleExists(string id)
        {
            return _context.Vehicle.Any(e => e.RegNumber == id);
        }
    }
}
