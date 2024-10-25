using System.ComponentModel.DataAnnotations;

namespace Project.PL.ViewModel
{
	public class ResetPaw
	{
		[Required]
		[DataType(DataType.Password)]
		[MaxLength(40)]
		public string Password { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[MaxLength(40)]
		[Compare(nameof(Password))]
		public string ConfirmPassWord { get; set; }

		public string Email { get; set; }
		public string Token { get; set; }

	}
}
