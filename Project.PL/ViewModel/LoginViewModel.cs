using System.ComponentModel.DataAnnotations;

namespace Project.PL.ViewModel
{
	public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [MaxLength(40)]
        [Display(Name = "Email Address ")]
        public string Email { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
