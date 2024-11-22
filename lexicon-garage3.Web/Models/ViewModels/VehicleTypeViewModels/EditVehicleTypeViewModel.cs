using lexicon_garage3.Core.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace lexicon_garage3.Web.Models.ViewModels.VehicleTypeViewModels
{
    public class EditVehicleTypeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Type Name is required.")]
        [StringLength(100)]
        [Display(Name = "Vehicle Type Name")]
        public string VehicleTypeName { get; set; }

        [Required(ErrorMessage = "Number of wheel is required.")]
        [Range(1, 30, ErrorMessage = "Number of wheels must be a positive integer.")]
        [Display(Name = "Number of wheels")]
        public int NumOfWheels { get; set; }

        [Required]
        public Size SelectedVehicleSize { get; set; } // enum value


        //public IEnumerable<SelectListItem> VehicleTypeSizes { get; set; } // dropdown list
        public List<SelectListItem> VehicleTypeSizes { get; set; } = new List<SelectListItem>(); // Initialize to avoid null errors

      
    }
}
