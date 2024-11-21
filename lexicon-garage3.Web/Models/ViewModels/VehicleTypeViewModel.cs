using lexicon_garage3.Core.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace lexicon_garage3.Web.Models.ViewModels
{
    public class VehicleTypeViewModel
    {

        public int Id { get; set; }
        [Required]
        public string VehicleTypeName { get; set; }

        // public string VehicleSize { get; set; }

        [Required]
        public int NumOfWheels { get; set; }

        [Required]
        public string SelectedVehicleSize { get; set; } // enum value

        
        //public IEnumerable<SelectListItem> VehicleTypeSizes { get; set; } // dropdown list
        public List<SelectListItem> VehicleTypeSizes { get; set; } = new List<SelectListItem>(); // Initialize to avoid null errors

    }
}
