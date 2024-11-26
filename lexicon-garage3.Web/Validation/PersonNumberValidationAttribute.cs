using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace lexicon_garage3.Web.Validation
{
    public class PersonNumberValidationAttribute : ValidationAttribute
    {
        private readonly string Pattern = @"^(19\d{2}|200\d|2006)(0[1-9]|1[0-2])([0-2][1-9]|3[01])\d{4}$";
        
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string personNumber = value.ToString().Trim();

            if (!Regex.IsMatch(personNumber, Pattern))
            {
                return new ValidationResult("Invalid Person Number format. The format should be YYYYMMDDXXXX");
            }

            return ValidationResult.Success;
        }
    }
}
  