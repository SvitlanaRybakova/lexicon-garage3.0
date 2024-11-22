using lexicon_garage3.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace lexicon_garage3.Web.Models.ViewModels.MembersViewModels
{
    public class IndexMemberViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Display(Name = "Vehicle Count")]
        public int VehicleCount { get; set; }

        [Display(Name = "Total Parkkng Cost")]
        public decimal TotalParkingCost { get; set; } = 0;

        public string SearchTerm { get; set; }

        public IEnumerable<IndexMemberViewModel> Members { get; set; } = new List<IndexMemberViewModel>();
    }
}
