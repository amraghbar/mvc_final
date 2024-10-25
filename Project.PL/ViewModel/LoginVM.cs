using System.ComponentModel.DataAnnotations;

namespace Project.PL.ViewModel
{
	public class LoginVM
	{
		
		[Required(ErrorMessage = "Email isRequired")]
		[MinLength(5)]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }
		[Required(ErrorMessage = "Password isRequired")]
		[DataType(DataType.Password)]

		public string Password { get; set; }
	
		public bool RememberMe { get; set; }
		
	}
}
