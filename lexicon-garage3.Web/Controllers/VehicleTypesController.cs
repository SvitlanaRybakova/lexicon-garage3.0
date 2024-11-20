using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lexicon_garage3.Core.Entities;
using lexicon_garage3.Persistance.Data;
using lexicon_garage3.Web.Models.ViewModels;


namespace lexicon_garage3.Web.Controllers
{
    public class VehicleTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VehicleTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: VehicleTypes
        public async Task<IActionResult> VehicleTypeIndex()
        {
            return View(await _context.VehicleType.ToListAsync());
        }

        // GET: VehicleTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleType = await _context.VehicleType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicleType == null)
            {
                return NotFound();
            }

            return View(vehicleType);
        }

        // GET: VehicleTypes/Create ** Populate the drop down list values for creation
        public IActionResult Create()
        {
            var model = new VehicleTypeViewModel
            {
                VehicleTypeSizes = Enum.GetValues(typeof(VehicleTypeSize))
                                   .Cast<VehicleTypeSize>()
                                   .Select(v => new SelectListItem
                                   {
                                       Text = v.ToString(),
                                       Value = v.ToString()
                                   })
                                   .ToList()
            };


            return View(model);
        }

        // POST: VehicleTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VehicleTypeName,SelectedVehicleSize,NumOfWheels")] VehicleTypeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var vehicleType = new VehicleType
                {
                    //Id = viewModel.Id,
                    VehicleTypeName = viewModel.VehicleTypeName,
                    VehicleSize = viewModel.SelectedVehicleSize,
                    NumOfWheels = viewModel.NumOfWheels
                    
                };
                _context.Add(vehicleType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(VehicleTypeIndex));
            }

            /**Replace with actual logging to find what error has occurred**/
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            foreach (var error in errors)
            {
                Console.WriteLine(error); 
            }

            viewModel.VehicleTypeSizes = Enum.GetValues(typeof(VehicleTypeSize))// repopulate the dropdown list if the creation fails
                                .Cast<VehicleTypeSize>()
                                .Select(v => new SelectListItem
                                {
                                    Text = v.ToString(),
                                    Value = v.ToString()
                                }).ToList();
                                

            return View(viewModel);
        }

        // GET: VehicleTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleType = await _context.VehicleType.FindAsync(id);
            if (vehicleType == null)
            {
                return NotFound();
            }
            return View(vehicleType);
        }

        // POST: VehicleTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VehicleTypeName,VehicleSize,NumOfWheels")] VehicleType vehicleType)
        {
            if (id != vehicleType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicleType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleTypeExists(vehicleType.Id))
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
            return View(vehicleType);
        }

        // GET: VehicleTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleType = await _context.VehicleType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicleType == null)
            {
                return NotFound();
            }

            return View(vehicleType);
        }

        // POST: VehicleTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicleType = await _context.VehicleType.FindAsync(id);
            if (vehicleType != null)
            {
                _context.VehicleType.Remove(vehicleType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleTypeExists(int id)
        {
            return _context.VehicleType.Any(e => e.Id == id);
        }
    }
}
