﻿using Microsoft.AspNetCore.Mvc;
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
            var applicationDbContext = _context.Vehicle.Include(v => v.VehicleType);
            return View(await applicationDbContext.ToListAsync());
        }

        
        public async Task<IActionResult> MemberIndex()
        {

            var user = await _userManager.GetUserAsync(User);

            // Check if the user is null
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var userId = user.Id;


            var vehiclesForUser = await _context.Vehicle
                .Where(v => v.MemberId == userId)
                .ToListAsync();
                        Console.WriteLine(vehiclesForUser.Count);
            //var applicationDbContext = _context.Vehicle.Include(v => v.VehicleType.VehicleTypeName);

            var memberViewData = await _context.Vehicle
                                    .Where(v => v.MemberId == userId)
                                    .Include(v => v.VehicleType) // Include related VehicleType
                                    .Select(v => new MemberIndexVehicleParkingViewModel
                                    {
                                        RegNumber = v.RegNumber,
                                        Color = v.Color,
                                        Brand = v.Brand,
                                        VehicleType = v.VehicleType.VehicleTypeName // Access VehicleType data
                                    }).ToListAsync(); // Filter by MemberId (userId)

            // Get roles
            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Contains("Admin"))
            {
                return View("AdminIndex", "Vehicles");
            }
            else
            {
                return View(memberViewData);
            }

            //return View("Index", "Vehicles");
           
           // return View(await applicationDbContext.ToListAsync());
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminIndex()
        {
            // Fetch data from each table

            var vehicleParkingData = await _context.Vehicle
                                    .Join(_context.Member,
                                          vehicle => vehicle.MemberId,
                                          member => member.Id,
                                          (vehicle, member) => new { vehicle, member })
                                    .Join(_context.VehicleType,
                                          vm => vm.vehicle.VehicleTypeId,
                                          vehicleType => vehicleType.Id,
                                          (vm, vehicleType) => new { vm.vehicle, vm.member, vehicleType })
                                    .Join(_context.ParkingSpot,
                                          vmt => vmt.vehicle.RegNumber,
                                          parkingSpot => parkingSpot.RegNumber,
                                          (vmt, parkingSpot) => new AdminIndexVehicleParkingViewModel
                                          {
                                              Owner = $"{vmt.member.FirstName} {vmt.member.LastName}",
                                              VehicleType = vmt.vehicleType.VehicleTypeName,
                                              RegNumber = vmt.vehicle.RegNumber,
                                              ParkingPlace = parkingSpot.ParkingNumber
                                          })
                                    .ToListAsync();

            return View(vehicleParkingData);
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
            var parkingSpots = _context.Set<ParkingSpot>()
                                .Where(p => p.IsAvailable);
            
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
                    
                };
                _context.Add(vehicle);

                //set owner of vehicle to the first user in the database, this should be instead the logged in user when you can log in
                var member = await _context.Member.FirstAsync();//TODO:change to logged in user id
                member.Vehicles.Add(vehicle);

                //set the parking spot
                var parkingSpot = await _context.ParkingSpot.FirstAsync(p => p.Id == viewModel.ParkingSpotId);
                parkingSpot.RegNumber = viewModel.RegNumber;
                parkingSpot.IsAvailable = false;
                parkingSpot.Vehicle = vehicle;

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ParkingReceipt), new { regNumber = vehicle.RegNumber });
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
                .FirstAsync(v => v.RegNumber == regNumber);

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
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598
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

                // Check the role and redirect
                if (User.IsInRole("Admin"))
                    return RedirectToAction("AdminIndex", "Vehicle");
                else 
                    return RedirectToAction("Index", "Vehicle");


                return RedirectToAction(nameof(Index));
            }
            ViewData["VehicleTypeId"] = new SelectList(_context.Set<VehicleType>(), "Id", "VehicleSize", vehicle.VehicleTypeId);
            return View(vehicle);
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
