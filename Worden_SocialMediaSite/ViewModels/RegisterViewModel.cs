using System.ComponentModel.DataAnnotations;

namespace Worden_SocialMediaSite.ViewModels
{
    public class RegisterViewModel : LoginViewModel
    {
        [Required(ErrorMessage = "You must enter a first name!")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "You must enter a last name!")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "You must enter an email address!")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }


    }
}
