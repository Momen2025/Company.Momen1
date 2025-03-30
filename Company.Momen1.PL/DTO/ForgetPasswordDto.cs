using System.ComponentModel.DataAnnotations;

namespace Company.Momen1.PL.DTO
{
    public class ForgetPasswordDto 
    {
        [Required(ErrorMessage = "Email Is Required !")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
