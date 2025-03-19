using Company.Momen1.BLL.Interfaces;
using Company.Momen1.DAL.Models;
using Company.Momen1.PL.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Company.Momen1.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository; //null
        //ASk CLR Create object From EmployeeREpository
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var employees = _employeeRepository.GetAll();
            return View(employees);
        }
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateEmployeeDTO model)
        {
            if(ModelState.IsValid ) //Server  Side  Vaildation
            {
                var employee = new Employee()
                {
                    Name=model.Name,
                    Address=model.Address,
                    Age=model.Age,
                    CreateAt=model.CreateAt,
                    HiringDate=model.HiringDate,
                    Email=model.Email,
                    IsActive=model.IsActive,
                    Phone=model.Phone,
                    Salary=model.Salary
                };
                var count = _employeeRepository.Add(employee);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Details(int? Id,string viewName = "Details")
        {
            if (Id is null) return BadRequest("Invalid ID "); //400
            var employee = _employeeRepository.Get(Id.Value);
            if (employee is null) return NotFound(new { StatusCode = 404, message = $"Employee With Id :{Id} is  not Found" });
            return View(viewName,employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int Id,CreateEmployeeDTO model)
        {
            if(ModelState.IsValid)
            {
                //if (Id !=model.Id) return BadRequest(); 
                var employee = new Employee()
                {
                    Name = model.Name,
                    Address = model.Address,
                    Age = model.Age,
                    CreateAt = model.CreateAt,
                    HiringDate = model.HiringDate,
                    Email = model.Email,
                    IsActive = model.IsActive,
                    Phone = model.Phone,
                    Salary = model.Salary
                };
                var count = _employeeRepository.Update(employee );
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
              
            }
            return View( model);
        }
        [HttpGet]
        public IActionResult Delete(int? Id)
        {
            return Details(Id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int Id, Employee model)
        {
            if (ModelState.IsValid)
            {
                if (Id != model.Id) return BadRequest();
                var count = _employeeRepository.Delete(model);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }

            }
            return View(model);
        }

    }
}
