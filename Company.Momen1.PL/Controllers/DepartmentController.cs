using Company.Momen1.BLL.Interfaces;
using Company.Momen1.BLL.Repositories;
using Company.Momen1.DAL.Models;
using Company.Momen1.PL.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Company.Momen1.PL.Controllers
{
    //MVC Controller 
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepositories;

        //ASk CLR Create Object from DepartmentRepositories
        public DepartmentController(IDepartmentRepository departmentRepositories)
        {
            _departmentRepositories = departmentRepositories;
        }
        [HttpGet] //GET :/Department/Index 
        public IActionResult Index()
        {
           
          var department = _departmentRepositories.GetAll();

            return View(department);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
           
        }
        [HttpPost]
        public IActionResult Create(CreateDepartmentDTo model)
        {
            if (ModelState.IsValid) //server Side validation
            {
                var deaprtment = new Department()
                {
                    Code=model.Code,
                    Name=model.Name,
                    CreateAt=model.CreateAt
                };

               var count= _departmentRepositories.Add(deaprtment);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);

        }
    }
}
