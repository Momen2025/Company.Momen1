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
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>) await _context.Employees.Include(E => E.Department).ToListAsync();
            }
           return await _context.Set<T>().ToListAsync();
        }
        public async Task<T?> GetAsync(int Id)
        {
            if (typeof(T) == typeof(Employee))
            {
                return await _context.Employees.Include(E => E.Department).FirstOrDefaultAsync(E=> E.Id == Id ) as T;
            }

            return _context.Set<T>().Find(Id);

        }
        //public async Task AddAsync(T model)
        //{
        //    await _context.Set<T>().AddAsync(model);
        //}
        public void Update(T model)
        {

            _context.Set<T>().Update(model);
          
        }
        public void Delete(T model)
        {

            _context.Set<T>().Remove(model);
           
        }

        public async Task AddASync(T model)
        {
            await _context.Set<T>().AddAsync(model);
        }
    }
}
