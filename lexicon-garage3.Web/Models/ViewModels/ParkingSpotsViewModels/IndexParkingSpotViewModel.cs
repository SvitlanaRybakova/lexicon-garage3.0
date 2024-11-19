using System.ComponentModel.DataAnnotations;

namespace lexicon_garage3.Web.Models.ViewModels.ParkingSpotsViewModels
{
    public class IndexParkingSpotViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Space Size")]
        public string Size { get; set; }

        [Display(Name = "Space Number")]
        public int ParkingNumber { get; set; }

        [Display(Name = "Is Available")]
        public bool IsAvailable { get; set; }

        [Display(Name = "Hourly Rate")]
        public int HourRate { get; set; }
    }
}
