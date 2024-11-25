using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lexicon_garage3.Core.Entities;
using lexicon_garage3.Persistance.Data;
using lexicon_garage3.Web.Models.ViewModels.VehicleViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace lexicon_garage3.Web.Controllers
{
    [Authorize]
    public class VehiclesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Member> _userManager;

        public VehiclesController(ApplicationDbContext context, UserManager<Member> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Vehicles
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);  // get logged in user
            if (user == null) return RedirectToAction("Login", "Account");   // if not logged in, send to login page

            
            var applicationDbContext = _context.Vehicle
                .Include(v => v.VehicleType)
                .Include(v => v.ParkingSpot)
                .Where(v => v.MemberId == user.Id);  // only get vehicles belonging to logged-in user
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
            ViewData["VehicleTypeId"] = new SelectList(_context.Set<VehicleType>(), "Id", "VehicleTypeName");
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
                //var regNumberExists = _context.Vehicle.FindAsync(viewModel.RegNumber) != null;
                var regNumberExists = await _context.Vehicle.AnyAsync(v => v.RegNumber == viewModel.RegNumber);
                if (regNumberExists)
                {
                    ViewData["VehicleTypeId"] = new SelectList(_context.Set<VehicleType>(), "Id", "VehicleTypeName");
                    ViewData["ErrorMessage"] = $"Registration number '{viewModel.RegNumber}' already exists!";
                    return View(viewModel);
                }
            

                // get logged in user so we can set it to own the vehicle
                var user = await _userManager.GetUserAsync(User);
                if (user == null) return RedirectToAction("Login", "Account");

                var vehicleType = await _context.VehicleType
                    .FirstOrDefaultAsync(m => m.Id == viewModel.VehicleTypeId);

                //make the Vehicle class from the viewmodel data etc
                var vehicle = new Vehicle
                {
                    RegNumber = viewModel.RegNumber,
                    Color = viewModel.Color,
                    Brand = viewModel.Brand,
                    Model = viewModel.Model,
                    ArrivalTime = DateTime.Now,
                    VehicleTypeId = viewModel.VehicleTypeId,
                    VehicleType = vehicleType,
                    MemberId = user.Id
                };
                _context.Add(vehicle);

                user.Vehicles.Add(vehicle);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new {regNumber = vehicle.RegNumber});
            }

            ViewData["VehicleTypeId"] =
                new SelectList(_context.Set<VehicleType>(), "Id", "VehicleSize", viewModel.VehicleTypeId);
            return View(viewModel);
        }

        // GET: Vehicles/ParkingReceipt
        public async Task<IActionResult> ParkingReceipt(string regNumber)
        {
            var vehicle = await _context.Vehicle
                .Include(v => v.VehicleType)
                .Include(v => v.ParkingSpot)
                .FirstOrDefaultAsync(v => v.RegNumber == regNumber);

            var parkingReceiptViewModel = new ParkingReceiptViewModel()
            {
                RegNumber = vehicle.RegNumber,
                Brand = vehicle.Brand,
                Model = vehicle.Model,
                Color = vehicle.Color,
                ArrivalTime = vehicle.ArrivalTime,
                CheckoutTime = vehicle.CheckoutTime,
                VehicleType = vehicle.VehicleType,
                ParkingSpot = vehicle.ParkingSpot
            };
            return View(parkingReceiptViewModel);
        }

        // GET: Vehicles/CheckOutReceipt
        public async Task<IActionResult> CheckOutReceipt(string id)
        {
            var vehicle = await _context.Vehicle
                .Include(v => v.VehicleType)
                .Include(v => v.ParkingSpot)
                .FirstOrDefaultAsync(v => v.RegNumber == id);

            var checkOutReceiptViewModel = new CheckOutReceiptViewModel()
            {
                RegNumber = vehicle.RegNumber,
                Brand = vehicle.Brand,
                Model = vehicle.Model,
                Color = vehicle.Color,
                ArrivalTime = vehicle.ArrivalTime,
                CheckoutTime = DateTime.Now,
                VehicleType = vehicle.VehicleType,
                ParkingSpot = vehicle.ParkingSpot
            };
            vehicle.CheckoutTime = checkOutReceiptViewModel.CheckoutTime;
            vehicle.ParkingSpot.RegNumber = null;
            vehicle.ParkingSpot.IsAvailable = true;
            await _context.SaveChangesAsync();
            return View(checkOutReceiptViewModel);
        }

        // GET: Vehicles/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var vehicle = await _context.Vehicle.FindAsync(id);
            var vehicle = await _context.Vehicle
                .Include(v => v.ParkingSpot)
                .Include(v => v.VehicleType)
                .FirstOrDefaultAsync(v => v.RegNumber == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            var viewModel = new EditVehicleViewModel()
            {
                RegNumber = vehicle.RegNumber,
                Color = vehicle.Color,
                Brand = vehicle.Brand,
                Model = vehicle.Model,
                ArrivalTime = vehicle.ArrivalTime,
                VehicleTypeId = vehicle.VehicleTypeId,
                ParkingSpotId = vehicle.ParkingSpot?.Id,
                ParkingSpot = vehicle.ParkingSpot
            };

            var parkingSpots = _context.Set<ParkingSpot>()
                .Where(p => p.IsAvailable && p.Size == vehicle.VehicleType.VehicleSize);

            ViewData["ParkingSpotId"] = new SelectList(parkingSpots, "Id", "ParkingNumber");

            ViewData["VehicleTypeId"] =
                new SelectList(_context.Set<VehicleType>(), "Id", "VehicleSize", vehicle.VehicleTypeId);

            return View(viewModel);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditVehicleViewModel viewModel)
        {
            //if (id != vehicle.RegNumber)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                try
                {
                    var vehicle = await _context.Vehicle.FirstAsync(v => v.RegNumber == viewModel.RegNumber);
                    vehicle.Brand = viewModel.Brand;
                    vehicle.Model = viewModel.Model;
                    vehicle.Color = viewModel.Color;
                    vehicle.VehicleTypeId = viewModel.VehicleTypeId;
                    vehicle.VehicleType = await _context.VehicleType.FirstAsync(t => t.Id == viewModel.VehicleTypeId);

                    if (vehicle.ParkingSpot == null)
                    {
                        //set the parking spot
                        var parkingSpot = await _context.ParkingSpot.FirstAsync(p => p.Id == viewModel.ParkingSpotId);
                        parkingSpot.RegNumber = viewModel.RegNumber;
                        parkingSpot.IsAvailable = false;
                        parkingSpot.Vehicle = vehicle;
                        vehicle.ParkingSpot = parkingSpot;
                        vehicle.ArrivalTime = DateTime.Now;
                    }

                    _context.Update(vehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(viewModel.RegNumber))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(ParkingReceipt), new {regNumber = viewModel.RegNumber});
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Vehicles/Delete/5
        [Authorize(Roles = "Admin")]
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