using System.ComponentModel.DataAnnotations;
using System.Reflection;
using lexicon_garage3.Core.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace lexicon_garage3.Web.Models.ViewModels.ParkingSpotsViewModels
{
    public class CreateParkingSpotsViewModel
    {

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
