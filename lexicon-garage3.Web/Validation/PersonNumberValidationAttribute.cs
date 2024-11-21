using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace lexicon_garage3.Web.Validation
{
    public class PersonNumberValidationAttribute : ValidationAttribute
    {
        private const string Pattern = @"^\d{8}-\d{4}$";

        public PersonNumberValidationAttribute()
        {
            this.ErrorMessage = "The Person Number must be in the format YYYYMMDD-XXXX.";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
  
            string personNumber = value.ToString();
            if (!Regex.IsMatch(personNumber, Pattern))
            {
                return new ValidationResult(ErrorMessage ?? "Invalid Person Number format.");
            }

            return ValidationResult.Success; 
        }
    }
}
