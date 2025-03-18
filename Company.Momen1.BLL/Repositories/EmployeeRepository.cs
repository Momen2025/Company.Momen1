using Company.Momen1.BLL.Interfaces;
using Company.Momen1.DAL.Data.Contexts;
using Company.Momen1.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Momen1.BLL.Repositories
{
    public class EmployeeRepository : GenaricRepository<Employee>,IEmployeeRepository
    {
        public EmployeeRepository(CompanyDbContext context ): base(context) //ASk CLR  CReate Object From ComapnyDbContext 
        {

        }
        //private readonly CompanyDbContext _context;

        //public EmployeeRepository(CompanyDbContext context)
        //{
        //    _context = context;
        //}
        //public IEnumerable<Employee> GetAll()
        //{
        //    return _context.Employees.ToList();
        //}
        //public Employee? Get(int Id)
        //{
        //    return _context.Employees.Find(Id);

        //}

        //public int Add(Employee model)
        //{
        //    _context.Employees.Add(model);
        //    return _context.SaveChanges();
        //}
        //public int Update(Employee model)
        //{
        //    _context.Employees.Update(model);
        //    return _context.SaveChanges();
        //}

        //public int Delete(Employee model)
        //{
        //    _context.Employees.Remove(model);
        //    return _context.SaveChanges();
        //}


    }
}
