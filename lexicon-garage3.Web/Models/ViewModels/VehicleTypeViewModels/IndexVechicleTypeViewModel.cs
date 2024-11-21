using System.ComponentModel.DataAnnotations;

namespace lexicon_garage3.Web.Models.ViewModels.VehicleTypeViewModels
{
    public class IndexVechicleTypeViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Vehicle Type Name")]
        public string VehicleTypeName { get; set; }

        [Display(Name = "Vehicle Size")]
        public string VehicleSize { get; set; }

        [Display(Name = "Number of wheels")]
        public int NumOfWheels { get; set; }

    }
}
