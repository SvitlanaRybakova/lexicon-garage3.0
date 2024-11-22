using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lexicon_garage3.Core.Entities;
using lexicon_garage3.Persistance.Data;
using lexicon_garage3.Web.Models.ViewModels.MembersViewModels;
using Microsoft.AspNetCore.Identity;


namespace lexicon_garage3.Web.Controllers
{
    public class MembersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Member> _userManager;

        public MembersController(ApplicationDbContext context, UserManager<Member> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Members
        public async Task<IActionResult> IndexMembers(string searchTerm)
        {
 
            var query = _context.Member
                .Include(m => m.Vehicles)
                .ThenInclude(v => v.ParkingSpot)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(m => m.FirstName.Contains(searchTerm) || m.LastName.Contains(searchTerm));
            }

            var members = await query.ToListAsync();

            var memberViewModels = members.Select(m => new IndexMemberViewModel
            {
                Id = m.Id,
                FullName = $"{m.FirstName} {m.LastName}",
                VehicleCount = m.Vehicles.Count,
                TotalParkingCost = m.Vehicles.Sum(v => CalculateParkingCost(v.ParkingSpot, v.ArrivalTime, v.CheckoutTime))
            }).ToList();

            
            var viewModel = new IndexMemberViewModel
            {
                SearchTerm = searchTerm,
                Members = memberViewModels
            };

            return View("Index", viewModel);
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

            var model = new EditMemberViewModel
            {
                Id = member.Id,
                FirstName = member.FirstName,
                LastName = member.LastName,
                UserName = member.UserName,
                PersonNumber = member.PersonNumber
            };
            return View(model);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, EditMemberViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var member = await _context.Member
                        .FirstOrDefaultAsync(m => m.Id == model.Id);

                    if (member == null)
                    {
                        TempData["ErrorMessage"] = "Member not found.";
                        return NotFound();
                    }
                    member.FirstName = model.FirstName;
                    member.LastName = model.LastName;
                    //member.UserName = model.UserName;
                    member.PersonNumber = model.PersonNumber;

                    _context.Update(member);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "The data updated successfully!";
                }
                catch (Exception)
                {
                    TempData["ErrorMessage"] = "An error occurred while updating the member.";
                    return View(model);
                }
            }
            return View(model);// in case not passing validation
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
            return RedirectToAction(nameof(IndexMembers));
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
