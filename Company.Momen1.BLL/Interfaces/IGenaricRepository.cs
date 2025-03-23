using Company.Momen1.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Momen1.BLL.Interfaces
{
   public interface IGenaricRepository<T> where T :BaseEntity
    {
       Task <IEnumerable<T>> GetAllAsync();
      Task<T?> GetAsync(int Id);

        Task  AddASync(T model);
        void Update(T model);
        void Delete(T model);
    }
}
