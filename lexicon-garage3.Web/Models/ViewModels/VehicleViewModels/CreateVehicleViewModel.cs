using System.ComponentModel.DataAnnotations;

namespace lexicon_garage3.Web.Models.ViewModels.VehicleViewModels;

public class CreateVehicleViewModel
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

    [Required(ErrorMessage = " Parking spot is required.")]
    [Display(Name = "Parking spot")]
    public string ParkingSpotId { get; set; }

}