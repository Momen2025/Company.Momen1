using Company.Momen1.BLL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Company.Momen1.PL.Controllers
{
    //MVC Controller 
    public class DepartmentController : Controller
    {
        private readonly DepartmentRepositories _departmentRepositories;

        //ASk CLR Create Object from DepartmentRepositories
        public DepartmentController(DepartmentRepositories departmentRepositories)
        {
            _departmentRepositories = departmentRepositories;
        }
        [HttpGet] //GET :/Department/Index 
        public IActionResult Index()
        {
           
          var department =  _departmentRepositories.GetAll();

            return View(department);
        }
    }
}
