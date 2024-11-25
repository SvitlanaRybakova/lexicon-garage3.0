using lexicon_garage3.Core.Entities;
using lexicon_garage3.Web.Areas.Identity.Pages.Account;
using System.ComponentModel.DataAnnotations;

namespace lexicon_garage3.Web.Validation
{
    public class CheckLastName : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            const string errorMessage = "First name and last name can't be the same!";
            if (value is string input)
            {
                var inputModel = validationContext.ObjectInstance as RegisterModel.InputModel;
                if (inputModel != null)
                {
                    return inputModel.FirstName != input
                        ? ValidationResult.Success
                        : new ValidationResult(errorMessage);
                }
            }
            return new ValidationResult("Something went wrong!");

        }
    }
}
