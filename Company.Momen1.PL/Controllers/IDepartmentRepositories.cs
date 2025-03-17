using Company.Momen1.DAL.Models;

namespace Company.Momen1.PL.Controllers
{
    public interface IDepartmentRepositories
    {
        IEnumerable<Department> GetAll();
    }
}