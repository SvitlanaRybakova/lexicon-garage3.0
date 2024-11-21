using lexicon_garage3.Web.Validation;
using System.ComponentModel.DataAnnotations;


namespace lexicon_garage3.Web.Models.ViewModels.MembersViewModels
{
    public class EditMemberViewModel
    {
        public string Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Personal Number")]
        [PersonNumberValidation(ErrorMessage = "Correct format is YYYYMMDDXXXX")]
        public string PersonNumber { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }
    }
}
