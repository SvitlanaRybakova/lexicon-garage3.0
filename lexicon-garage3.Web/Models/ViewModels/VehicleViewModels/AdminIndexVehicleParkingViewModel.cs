using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace lexicon_garage3.Web.Models.ViewModels.VehicleViewModels
{
    public class AdminIndexVehicleParkingViewModel
    {
        [Display(Name = "Owner")]
        public string Owner { get; set; }         // From Member table

        [Display(Name = "Vehicle Type")]
        public string VehicleType { get; set; }   // From VehicleType table

        [Display(Name = "Registration Number")]
        public string RegNumber { get; set; }     // From Vehicle table

        [Display(Name = "Parking Place")]
        public int ParkingPlace { get; set; }  // From ParkingSpot table

    }
}
