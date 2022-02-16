using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.ViewModels
{
    public class SignUpViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required]
        //[StringLength(255,"", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare(nameof(Password), ErrorMessage = "Password did not match")]
        public string ConmfirmPassword { get; set; }
    }
}
