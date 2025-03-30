using System.ComponentModel.DataAnnotations;

namespace Company.Momen1.PL.DTO
{
    public class SignUpDto
    {
        [Required(ErrorMessage ="UserName Is Required !")]
        public string  UserName { get; set; }

        [Required(ErrorMessage = "FiestName Is Required !")]
        public string  FiestName { get; set; }

        [Required(ErrorMessage = "LastName Is Required !")]
        public string  LastName { get; set; }

        [Required(ErrorMessage = "Email Is Required !")]
        [EmailAddress]
        public string  Email { get; set; }

        [Required(ErrorMessage = "Password Is Required !")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password Is Required !")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Conformed Password dose not match the passwprd !")]
        public string ConfirmPassword { get; set; }


        [Required(ErrorMessage = "FiestName Is Required !")]
        public bool  IsAgrre { get; set; }
       
    }
}
