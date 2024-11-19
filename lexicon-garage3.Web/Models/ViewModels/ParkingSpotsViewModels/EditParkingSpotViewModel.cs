using System.ComponentModel.DataAnnotations;
using lexicon_garage3.Core.Entities;

namespace lexicon_garage3.Web.Models.ViewModels.ParkingSpotsViewModels
{
    public class EditParkingSpotViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Size is required.")]
        [Display(Name = "Space Size")]
        public Size Size { get; set; }

        [Required(ErrorMessage = "Space Number is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Parking Number must be a positive integer.")]
        [Display(Name = "Parking Number")]
        public int ParkingNumber { get; set; }

        [Required(ErrorMessage = "Hourly Rate is required.")]
        [Range(0, 1000, ErrorMessage = "Hourly Rate must be between 0 and 1000.")]
        [Display(Name = "Hourly Rate")]
        public int HourRate { get; set; }

        public bool IsAvailable { get; set; }
        public string? RegNumber { get; set; }
    }
}
