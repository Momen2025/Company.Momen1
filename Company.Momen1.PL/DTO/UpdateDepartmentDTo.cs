using System.ComponentModel.DataAnnotations;

namespace Company.Momen1.PL.DTO
{
    public class UpdateDepartmentDTo
    {
        [Required(ErrorMessage = "Code Is Required ! ")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Name Is Required ! ")]
        public string Name { get; set; }

        [Required(ErrorMessage = "CreateAt Is Required ! ")]
        public DateTime CreateAt { get; set; }

    }
}
