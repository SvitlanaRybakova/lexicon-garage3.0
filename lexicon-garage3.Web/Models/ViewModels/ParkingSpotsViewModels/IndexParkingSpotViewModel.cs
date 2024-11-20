using lexicon_garage3.Core.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

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


        public IEnumerable<ParkingSpot> ParkingSpots { get; set; } = new List<ParkingSpot>();
        public IEnumerable<SelectListItem> Sizes { get; set; } = GetSizes();

        private static IEnumerable<SelectListItem> GetSizes()
        {
            return Enum.GetValues(typeof(Size))
                .Cast<Size>()
                .Select(v =>
                {
                    var displayAttribute = v.GetType().GetField(v.ToString())
                                            .GetCustomAttributes<DisplayAttribute>(false)
                                            .FirstOrDefault();

                    return new SelectListItem
                    {
                        Text = displayAttribute?.Name ?? v.ToString(),
                        Value = v.ToString()
                    };
                })
                .ToList();
        }
    }
}
