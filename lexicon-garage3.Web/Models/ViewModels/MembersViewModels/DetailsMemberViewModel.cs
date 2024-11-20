namespace lexicon_garage3.Web.Models.ViewModels.MembersViewModels
{
    public class DetailsMemberViewModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public int PersonNumber { get; set; }
        public List<VehicleDetailsViewModel> Vehicles { get; set; }
    }

    public class VehicleDetailsViewModel
    {
        
        public string RegNumber { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public DateTime ArrivalTime { get; set; }
        public DateTime CheckoutTime { get; set; }
        public decimal ParkingCost { get; set; }
        public string ParkingSpotName { get; set; }
        public decimal CostPerHour { get; set; }
    }
}
