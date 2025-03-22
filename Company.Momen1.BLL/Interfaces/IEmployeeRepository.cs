using Company.Momen1.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Momen1.BLL.Interfaces
{
   public interface IEmployeeRepository :IGenaricRepository<Employee>
    {
        List<Employee>? GetByName(string name);
        //Employee? GetByName(string name);
        //IEnumerable<Employee> GetAll();
        //Employee? Get(int Id);

        //int Add(Employee model);
        //int Update(Employee model);
        //int Delete(Employee model);
    }
}
