using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lexicon_garage3.Core.Entities;
using lexicon_garage3.Persistance.Data;

namespace lexicon_garage3.Web.Controllers
{
    public class MembersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MembersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Members
        public async Task<IActionResult> IndexMembers()
        {
            var members = _context.Member
             .Include(m => m.Vehicles)
             .ThenInclude(v => v.ParkingSpot)
             .ToList();

            var memberViewModels = members.Select(m => new IndexMemberViewModel
            {
                Id = m.Id,
                FullName = $"{m.FirstName} {m.LastName}",
                VehicleCount = m.Vehicles.Count,
                TotalParkingCost = m.Vehicles.Sum(v => CalculateParkingCost(v.ParkingSpot, v.ArrivalTime, v.CheckoutTime))
            }).ToList();

            return View("Index", memberViewModels);
        }


        // GET: Members/Details/5
        public async Task<IActionResult> Details(string id)
        {
            var member = _context.Member
         .Include(m => m.Vehicles)
         .ThenInclude(v => v.ParkingSpot)
         .FirstOrDefault(m => m.Id == id);

            if (member == null)
            {
                return NotFound();
            }

            var viewModel = new DetailsMemberViewModel
            {
                Id = member.Id,
                FullName = $"{member.FirstName} {member.LastName}",
                UserName = member.UserName,
                PersonNumber = member.PersonNumber,
                Vehicles = member.Vehicles.Select(v => new VehicleDetailsViewModel
                {
                    RegNumber = v.RegNumber,
                    Brand = v.Brand,
                    Model = v.Model,
                    Color = v.Color,
                    ArrivalTime = v.ArrivalTime,
                    CheckoutTime = v.CheckoutTime,
                    ParkingCost = CalculateParkingCost(v.ParkingSpot, v.ArrivalTime, v.CheckoutTime),
                    ParkingSpotName = v.ParkingSpot?.RegNumber,
                    CostPerHour = v.ParkingSpot?.HourRate ?? 0
                }).ToList()
            };

            return View(viewModel);
        }


        // GET: Members/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Member.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,FirstName,LastName,PersonNumber,UserName")] Member member)
        {
            if (id != member.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(member);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExists(member.Id))
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
            return View(member);
        }

        // GET: Members/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Member
                .FirstOrDefaultAsync(m => m.Id == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var member = await _context.Member.FindAsync(id);
            if (member != null)
            {
                _context.Member.Remove(member);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberExists(string id)
        {
            return _context.Member.Any(e => e.Id == id);
        }

        private decimal CalculateParkingCost(ParkingSpot parkingSpot, DateTime arrivalTime, DateTime checkoutTime)
        {
            var parkingDuration = checkoutTime - arrivalTime;
            var hoursParked = (decimal)parkingDuration.TotalHours;

            return hoursParked * (parkingSpot?.HourRate ?? 0); 
        }
    }
}