using Company.Momen1.BLL.Interfaces;
using Company.Momen1.BLL.Repositories;
using Company.Momen1.DAL.Models;
using Company.Momen1.PL.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.Threading.Tasks;

namespace Company.Momen1.PL.Controllers
{
    //MVC Controller 
    [Authorize(Roles ="admon")]
    public class DepartmentController : Controller
    {
        //private readonly IDepartmentRepository _departmentRepositories;
        private readonly IUnitOfWork _unitOfWork;

        public IUnitOfWork UnitOfWork { get; }

        //ASk CLR Create Object from DepartmentRepositories
        public DepartmentController(/*IDepartmentRepository departmentRepositories*/IUnitOfWork unitOfWork)
        {
            //_departmentRepositories = departmentRepositories;
            _unitOfWork = unitOfWork;
        }
        [HttpGet] //GET :/Department/Index 
        public async Task<IActionResult> Index()
        {
           
          var department =await _unitOfWork.DepartmentRepository.GetAllAsync();

            return View(department);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
           
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateDepartmentDTo model)
        {
            if (ModelState.IsValid) //server Side validation
            {
                var deaprtment = new Department()
                {
                    Code=model.Code,
                    Name=model.Name,
                    CreateAt=model.CreateAt
                };

             await _unitOfWork.DepartmentRepository.AddASync(deaprtment);
                var count =await _unitOfWork.CompleteAsync();
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);

        }

        [HttpGet]
        public async Task<IActionResult> Details(int? Id,string viewName="Details")
        {
            if (Id is null) return BadRequest("Invalid Id"); //400

            var department=await _unitOfWork.DepartmentRepository.GetAsync(Id.Value);
            if (department is null) return NotFound(new { StatusCode = 404, message = $"Department with Id :{Id} is not found" });

            return View(viewName,department);
        }

        [HttpGet]
        public async Task<IActionResult> Edit (int? Id)
        {
            if (Id is null) return BadRequest("Invalid Id"); //400

            var department =await _unitOfWork.DepartmentRepository.GetAsync(Id.Value);
            if (department is null) return NotFound(new { StatusCode = 404, message = $"Department with Id :{Id} is not found" });

            var dto = new CreateDepartmentDTo()
            {
                Name = department.Name,
                Code = department.Code,
                CreateAt = department.CreateAt
            };
            return View(dto);

        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] int Id, CreateDepartmentDTo model)
        {

            if (ModelState.IsValid)
            {
                var department = new Department()
                {
                    Id = Id,
                    Name=model.Name,
                    CreateAt=model.CreateAt
                };       
                
                _unitOfWork.DepartmentRepository.Update(department);
                var count =await _unitOfWork.CompleteAsync();
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);

        }

        //[HttpPost]
        ////[ValidateAntiForgeryToken]
        //public IActionResult Edit([FromRoute] int Id, UpdateDepartmentDTo model)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        var department = new Department()
        //        {
        //            Id = Id,
        //            Name=model.Name,
        //            Code=model.Code,
        //            CreateAt=model.CreateAt
        //        };
        //        var count = _departmentRepositories.Update(department);
        //        if (count > 0)
        //        {
        //            return RedirectToAction(nameof(Index));
        //        }

        //return View(model);

        [HttpGet]
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id is null) return BadRequest("Invalid Id"); //400

            var department =await _unitOfWork.DepartmentRepository.GetAsync(Id.Value);
            if (department is null) return NotFound(new { StatusCode = 404, message = $"Department with Id :{Id} is not found" });

            var dto = new CreateDepartmentDTo()
            {
                Name = department.Name,
                Code = department.Code,
                CreateAt = department.CreateAt
            };
            return View(dto);

        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int Id, CreateDepartmentDTo model)
        {

            if (ModelState.IsValid)
            {
                var department = new Department()
                {
                    Id=Id,
                    Name = model.Name,
                    Code = model.Code,
                    CreateAt = model.CreateAt
                };
                           
                _unitOfWork.DepartmentRepository.Delete(department);
                var count =await _unitOfWork.CompleteAsync();
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }

            }
            return View(model);
        }

    }

}
