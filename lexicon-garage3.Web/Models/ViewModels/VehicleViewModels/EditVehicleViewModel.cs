using System.ComponentModel.DataAnnotations;
using lexicon_garage3.Core.Entities;

namespace lexicon_garage3.Web.Models.ViewModels.VehicleViewModels;

public class EditVehicleViewModel
{
    [Required(ErrorMessage = "Registration number is required.")]
    [Display(Name = "Registration number")]
    public string RegNumber { get; set; }

    [Required(ErrorMessage = "Color is required.")]
    [Display(Name = "Color")]
    public string Color { get; set; }

    [Required(ErrorMessage = " Vehicle brand is required.")]
    [Display(Name = "Brand")]
    public string Brand { get; set; }

    [Required(ErrorMessage = " Vehicle model is required.")]
    [Display(Name = "Model")]
    public string Model { get; set; }

    [Required(ErrorMessage = " Vehicle type is required.")]
    [Display(Name = "Vehicle type")]
    public int VehicleTypeId { get; set; }
    public DateTime? ArrivalTime { get; set; }
    public DateTime? CheckoutTime { get; set; }

    [Required(ErrorMessage = " Parking spot is required.")]
    [Display(Name = "Parking spot")]
    public string? ParkingSpotId { get; set; }
    public ParkingSpot? ParkingSpot { get; set; }

    public bool HasEverCheckedIn => ArrivalTime != null;
    public bool HasEverCheckedOut => CheckoutTime != null;
    public bool IsCheckedIn => ParkingSpotId != null;

}