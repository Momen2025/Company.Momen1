using System.ComponentModel.DataAnnotations;

namespace Company.Momen1.PL.DTO
{
    public class SignInDto
    {   
        [EmailAddress]
        [Required(ErrorMessage = "Email Is Required !")]        
        public string Email { get; set; }

        [Required(ErrorMessage = "Password Is Required !")]
        [DataType(DataType.Password)] //********
        public string Password { get; set; }

        public bool RemeberMe { get; set; }
    }
}
