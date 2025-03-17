using Company.Momen1.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Momen1.BLL.Interfaces
{
   public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAll();
        Department? Get(int Id);

        int Add(Department model);
        int Update(Department model);
        int Delete(Department model);
    }
}
