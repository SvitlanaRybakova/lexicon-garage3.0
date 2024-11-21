using System.ComponentModel.DataAnnotations;

namespace lexicon_garage3.Web.Models.ViewModels.ParkingSpotsViewModels
{
    public class SearchParkingSpotViewModel
    {
        [Display(Name = "Space Size")]
        public string Size { get; set; }

        [Display(Name = "Space Number")]
        public int ParkingNumber { get; set; }
    }
}
