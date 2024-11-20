using System.ComponentModel.DataAnnotations;

namespace lexicon_garage3.Web.Models.ViewModels.ParkingSpotsViewModels
{
    public class ParkingStatisticsViewModel
    {
        [Display(Name = "Total Spots")]
        public int TotalSpots { get; set; }

        [Display(Name = "Available Spots")]
        public int AvailableSpots { get; set; }

        [Display(Name = "Occupied Spots")]
        public int OccupiedSpots { get; set; }
    }
}
