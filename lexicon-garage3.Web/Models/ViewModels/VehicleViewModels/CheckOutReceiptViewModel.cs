using System.ComponentModel.DataAnnotations;
using lexicon_garage3.Core.Entities;

namespace lexicon_garage3.Web.Models.ViewModels.VehicleViewModels;

public class CheckOutReceiptViewModel
{
    public string RegNumber { get; set; }

    public string Color { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public DateTime ArrivalTime { get; set; }
    public DateTime CheckoutTime { get; set; }

    public VehicleType VehicleType { get; set; }
    public ParkingSpot ParkingSpot { get; set; }

    public TimeSpan TotalTime => CheckoutTime - ArrivalTime;

    public string TotalTimeString
    {
        get
        {
            var timeSpan = CheckoutTime - ArrivalTime;
            return $"{(int)timeSpan.TotalHours} hours {timeSpan.Minutes} minutes";
        }
    }

    public string TotalCost => (TotalTime.TotalHours * ParkingSpot.HourRate).ToString("F2");
}