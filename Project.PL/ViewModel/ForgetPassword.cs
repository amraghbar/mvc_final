using System.ComponentModel.DataAnnotations;

namespace Project.PL.ViewModel
{
    public class ForgetPassword
    {


        [Required(ErrorMessage = "Email isRequired")]
        [MinLength(5)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
