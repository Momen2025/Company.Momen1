using System.ComponentModel.DataAnnotations;

namespace Company.Momen1.PL.DTO
{
    public class ResetPasswprdDto
    {
        
        [Required(ErrorMessage = "Password Is Required !")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }


        [Required(ErrorMessage = "Password Is Required !")]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = "Conformed Password dose not match the passwprd !")]
        public string ConfirmPassword { get; set; }
    }
}
