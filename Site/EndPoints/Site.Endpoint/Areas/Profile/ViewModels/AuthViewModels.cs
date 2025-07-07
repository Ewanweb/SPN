using System.ComponentModel.DataAnnotations;

namespace Site.Endpoint.Areas.Profile.ViewModels
{
    public class AuthViewModels
    {

    }

    public class RegisterViewModel
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {

        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
