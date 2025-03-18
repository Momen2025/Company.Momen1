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
    public class DepartmentRepositories : IDepartmentRepository
    {
        private CompanyDbContext _context; //null
        //ASK CLR Create Object From CompanyDbContext
        
        public DepartmentRepositories(CompanyDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Department> GetAll()
        {
           return _context.Departments.ToList();
        }
        public Department? Get(int Id)
        {
            return _context.Departments.Find(Id);
        }
        public int Add(Department model)
        {
            _context.Departments.Add(model);
            return _context.SaveChanges();
        }
        public int Update(Department model)
        {
            _context.Departments.Update(model);
            return _context.SaveChanges();
        }
        public int Delete(Department model)
        {
            _context.Departments.Remove(model);
            return _context.SaveChanges();
        }

    }
}
