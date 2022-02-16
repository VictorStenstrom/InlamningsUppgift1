using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.ViewModels
{
    public class SignInViewModel
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
        public string ReturnUrl { get; set; }
    }
}
