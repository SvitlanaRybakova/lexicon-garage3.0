using System.ComponentModel.DataAnnotations;
using lexicon_garage3.Core.Entities;

namespace lexicon_garage3.Web.Models.ViewModels.VehicleViewModels;

public class ParkingReceiptViewModel
{
    public string RegNumber { get; set; }

    public string Color { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public DateTime ArrivalTime { get; set; }
    public DateTime CheckoutTime { get; set; }

    public VehicleType VehicleType { get; set; }
    public ParkingSpot ParkingSpot { get; set; } 

}