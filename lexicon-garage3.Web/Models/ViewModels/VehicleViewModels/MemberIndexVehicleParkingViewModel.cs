using System.ComponentModel.DataAnnotations;

namespace lexicon_garage3.Web.Models.ViewModels.VehicleViewModels
{
    public class MemberIndexVehicleParkingViewModel
    {
        [Display(Name = "Registration Number")]
        public string RegNumber { get; set; }
        public string Brand { get; set; }

        public string Color { get; set; }

        [Display(Name = "Vehicle Type")]
        public string VehicleType {  get; set; }
    }
}
