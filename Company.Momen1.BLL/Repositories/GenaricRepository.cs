using Company.Momen1.BLL.Interfaces;
using Company.Momen1.DAL.Data.Contexts;
using Company.Momen1.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Momen1.BLL.Repositories
{
    public class GenaricRepository<T> : IGenaricRepository<T> where T : BaseEntity
    {
        private readonly CompanyDbContext _context;

        public GenaricRepository(CompanyDbContext context)
        {
            _context = context;
        }
        public IEnumerable<T> GetAll()
        {
            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>)_context.Employees.Include(E => E.Department).ToList();
            }
           return _context.Set<T>().ToList();
        }
        public T? Get(int Id)
        {
            if (typeof(T) == typeof(Employee))
            {
                return  _context.Employees.Include(E => E.Department).FirstOrDefault(E=> E.Id == Id ) as T;
            }

            return _context.Set<T>().Find(Id);

        }
        public int Add(T model)
        {
             _context.Set<T>().Add(model);
            return _context.SaveChanges();

        }
        public int Update(T model)
        {

            _context.Set<T>().Update(model);
            return _context.SaveChanges();
        }
        public int Delete(T model)
        {

            _context.Set<T>().Remove(model);
            return _context.SaveChanges();
        }
    }
}
