using lexicon_garage3.Core.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace lexicon_garage3.Web.Models.ViewModels.VehicleTypeViewModels
{
    public class CreateVehicleTypeViewModel
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Vehicle Type Name is required.")]
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
