using Microsoft.Build.Framework;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Company.Momen1.PL.DTO
{
    public class CreateEmployeeDTO
    {

        
        public string Name { get; set; }
        [Range(22,60, ErrorMessage ="Age Must Be Between 22 And 60")]
        public int? Age { get; set; }
        [DataType(DataType.EmailAddress , ErrorMessage = "Email Is not Vaild !")]
        public string Email { get; set; }
        [RegularExpression(@"[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",ErrorMessage ="Address must Be  like  123-ST-City-Country")]
        public string Address { get; set; }
        [Phone]
        public int Phone { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        [DisplayName("HiringDate")]
        public DateTime HiringDate { get; set; }
        [DisplayName("Date Of Creation")]

        public DateTime CreateAt { get; set; }

        public int? DepartmentId { get; set; }
    }
}
