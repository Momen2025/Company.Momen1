using AutoMapper;
using Company.Momen1.DAL.Models;

namespace Company.Momen1.PL.DTO.Mapping
{
    //CLR 
    public class EmployeePropfile : Profile
    {
       public EmployeePropfile()
        {
            CreateMap<CreateEmployeeDTO, Employee>();

            CreateMap<Employee, CreateEmployeeDTO>();
        }
    }
}
