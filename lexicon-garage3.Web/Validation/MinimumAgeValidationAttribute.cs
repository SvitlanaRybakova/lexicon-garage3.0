using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace lexicon_garage3.Web.Validation
{
    public class MinimumAgeValidationAttribute : ValidationAttribute
    {
        private readonly int _minimumAge;

        public MinimumAgeValidationAttribute(int minimumAge)
        {
            _minimumAge = minimumAge;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string personNumber && personNumber.Length >= 8)
            {
                try
                {
                    // Extract date of birth from person number
                    string dobString = personNumber.Substring(0, 8); // First 8 characters (YYYYMMDD)
                    DateTime dob = DateTime.ParseExact(dobString, "yyyyMMdd", CultureInfo.InvariantCulture);

                    // Calculate the age
                    DateTime today = DateTime.Today;
                    int age = today.Year - dob.Year;
                    if (dob > today.AddYears(-age)) age--;

                    if (age < _minimumAge)
                    {
                        return new ValidationResult($"Person must be at least {_minimumAge} years old.");
                    }

                    return ValidationResult.Success;
                }
                catch (Exception)
                {
                    return new ValidationResult("Invalid date format in Personal Number.");
                }
            }

            return new ValidationResult("Invalid Personal Number format.");
        }
    }
}
