using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lexicon_garage3.Core.Entities;
using lexicon_garage3.Persistance.Data;
using lexicon_garage3.Web.Models.ViewModels.VehicleTypeViewModels;


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
            
            var vehicleType = await _context.VehicleType.ToListAsync();

            var viewModel = vehicleType.Select(p => new IndexVechicleTypeViewModel
            {   
                Id = p.Id,
                VehicleTypeName = p.VehicleTypeName,
                VehicleSize = p.VehicleSize,
                NumOfWheels = p.NumOfWheels

            });

            return View(viewModel);
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
            var model = new CreateVehicleTypeViewModel
            {
                VehicleTypeSizes = Enum.GetValues(typeof(Size))
                                   .Cast<Size>()
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
        public async Task<IActionResult> Create([Bind("Id,VehicleTypeName,SelectedVehicleSize,NumOfWheels")] CreateVehicleTypeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var vehicleType = new VehicleType
                {
                    //Id = viewModel.Id,
                    VehicleTypeName = viewModel.VehicleTypeName,
                    VehicleSize = viewModel.SelectedVehicleSize.ToString(),
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

            viewModel.VehicleTypeSizes = Enum.GetValues(typeof(Size))// repopulate the dropdown list if the creation fails
                                .Cast<Size>()
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

            var viewModel = new EditVehicleTypeViewModel
            {
                Id = vehicleType.Id,
                VehicleTypeName = vehicleType.VehicleTypeName,
                NumOfWheels = vehicleType.NumOfWheels,
                SelectedVehicleSize = Enum.Parse<Size>(vehicleType.VehicleSize),
                VehicleTypeSizes = Enum.GetValues(typeof(Size))
                                   .Cast<Size>()
                                   .Select(size => new SelectListItem
                                   {
                                       Value = size.ToString(),
                                       Text = size.ToString(),
                                       Selected = size.ToString() == vehicleType.VehicleSize
                                   }).ToList()
            };

            return View(viewModel);
        }

        // POST: VehicleTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VehicleTypeName,SelectedVehicleSize,NumOfWheels")] EditVehicleTypeViewModel editVehicleTypeView)
        {
            if (id != editVehicleTypeView.Id)
            {
                return NotFound();
            }
            

            if (ModelState.IsValid)
            {
                try
                {

                    var vehicleTypeModel = await _context.VehicleType.FindAsync(editVehicleTypeView.Id);
                    if (vehicleTypeModel == null)
                    {
                        return NotFound();
                    }

                    vehicleTypeModel.VehicleTypeName = editVehicleTypeView.VehicleTypeName;
                    vehicleTypeModel.VehicleSize = editVehicleTypeView.SelectedVehicleSize.ToString();
                    vehicleTypeModel.NumOfWheels = editVehicleTypeView.NumOfWheels;

                    _context.Update(vehicleTypeModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleTypeExists(editVehicleTypeView.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(VehicleTypeIndex));
            }
            return View(editVehicleTypeView);
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
            return RedirectToAction(nameof(VehicleTypeIndex));
        }

        private bool VehicleTypeExists(int id)
        {
            return _context.VehicleType.Any(e => e.Id == id);
        }
    }
}
