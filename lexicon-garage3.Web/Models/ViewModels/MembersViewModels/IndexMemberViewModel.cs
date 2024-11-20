namespace lexicon_garage3.Web.Models.ViewModels.MembersViewModels
{
    public class IndexMemberViewModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public int VehicleCount { get; set; }
        public decimal TotalParkingCost { get; set; } = 0;
    }
}
