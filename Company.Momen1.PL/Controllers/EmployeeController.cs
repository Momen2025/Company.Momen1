using AutoMapper;
using Company.Momen1.BLL.Interfaces;
using Company.Momen1.DAL.Models;
using Company.Momen1.PL.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Company.Momen1.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository; 
        //private readonly IDepartmentRepositories _departmentRepositories;
        private readonly IDepartmentRepository _departmentRepositories;
        private readonly IMapper _mapper;



        //ASk CLR Create object From EmployeeREpository
        public EmployeeController(
            IEmployeeRepository employeeRepository,
            IDepartmentRepository departmentRepositories,
            IMapper mapper
            
            )
        {
            _employeeRepository = employeeRepository;
            _departmentRepositories = departmentRepositories;
            _mapper = mapper;
        }
        [HttpGet]  //Get : /Department/Index
        public IActionResult Index(string? SearchInput)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchInput))
            {
                  employees = _employeeRepository.GetAll();
            }
            else
            {
                 employees = _employeeRepository.GetByName(SearchInput);


            }

            //Dictionary : 3 Property
            // 1. ViewData : Trasfer Extra Info From Cntroller (Action) To View 
            //ViewData["Message"] = "Hello From ViewData";

            // 2. ViewBag  : Trasfer Extra Info From Cntroller (Action) To View 
            //ViewBag.Message = "Hello From ViewBag";

            return View(employees);
        }
        [HttpGet]
        public IActionResult Create()
        {
           var departments = _departmentRepositories.GetAll();
            ViewData["department"] = departments;
           

            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateEmployeeDTO model)
        {
            if(ModelState.IsValid ) //Server  Side  Vaildation
            {

                //Manual MApping

                //var employee = new Employee()
                //{
                //    Name = model.Name,
                //    Address = model.Address,
                //    Age = model.Age,
                //    CreateAt = model.CreateAt,
                //    HiringDate = model.HiringDate,
                //    Email = model.Email,
                //    IsActive = model.IsActive,
                //    Phone = model.Phone,
                //    Salary = model.Salary,
                //    DepartmentId = model.DepartmentId
                //};

                var employee = _mapper.Map<Employee>(model);
                var count = _employeeRepository.Add(employee);
                if (count > 0)
                 {
                     TempData["Message"] = "Employee is Create !";
                      return RedirectToAction(nameof(Index));
                  }
              
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Details(int? Id,string viewName = "Details")
        {

            var departments = _departmentRepositories.GetAll();
            //ViewData["department"] = departments;
            ViewBag.Departments = departments;


            if (Id is null) return BadRequest("Invalid ID "); //400
            var employee = _employeeRepository.Get(Id.Value);
            if (employee is null) return NotFound(new { StatusCode = 404, message = $"Employee With Id :{Id} is  not Found" });
            
            return View(employee);
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
                if ( Id != model.Id) return BadRequest();
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
