using System.ComponentModel.DataAnnotations;

namespace Project.PL.ViewModel
{
	public class RegisterVM
	{
		[Required(ErrorMessage = "UserNAme isRequired")]
		public string UserName { get; set; }
		[Required(ErrorMessage = "Email isRequired")]
		[MinLength(5)]
		[DataType(DataType.EmailAddress)]

		public string Email { get; set; }
		[Required(ErrorMessage = "Password isRequired")]
		[DataType(DataType.Password)]

		public string Password { get; set; }
		[Required(ErrorMessage = "Password isRequired")]
		[DataType(DataType.Password)]
		[Compare(nameof(Password))]

		public string ConfirmPassword { get; set; }


	}
}
