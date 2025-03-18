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
    public class GenaricRepository<T> : IGenaricRepository<T> where T : BaseEntity
    {
        private readonly CompanyDbContext _context;

        public GenaricRepository(CompanyDbContext context)
        {
            _context = context;
        }
        public IEnumerable<T> GetAll()
        {
           return _context.Set<T>().ToList();
        }
        public T? Get(int Id)
        {
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
