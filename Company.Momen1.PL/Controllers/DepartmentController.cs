using Company.Momen1.BLL.Interfaces;
using Company.Momen1.BLL.Repositories;
using Company.Momen1.DAL.Models;
using Company.Momen1.PL.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

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

        [HttpGet]
        public IActionResult Details(int? Id,string viewName="Details")
        {
            if (Id is null) return BadRequest("Invalid Id"); //400

            var department= _departmentRepositories.Get(Id.Value);
            if (department is null) return NotFound(new { StatusCode = 404, message = $"Department with Id :{Id} is not found" });

            return View(viewName,department);
        }

        [HttpGet]
        public IActionResult Edit (int? Id)
        {
        //    if (Id is null) return BadRequest("Invalid Id"); //400

        //    var department = _departmentRepositories.Get(Id.Value);
        //    if (department is null) return NotFound(new { StatusCode = 404, message = $"Department with Id :{Id} is not found" });

            return Details(Id,"Edit");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int Id, Department department)
        {

            if (ModelState.IsValid)
            {
                if (Id != department.Id) return BadRequest(); //400         
                var count = _departmentRepositories.Update(department);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }



            }
            return View(department);

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
        public IActionResult Delete(int? Id)
        {
            //if (Id is null) return BadRequest("Invalid Id"); //400

            //var department = _departmentRepositories.Get(Id.Value);
            //if (department is null) return NotFound(new { StatusCode = 404, message = $"Department with Id :{Id} is not found" });

            return Details(Id,"Delete");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int Id, Department department)
        {

            if (ModelState.IsValid)
            {
                if (Id != department.Id) return BadRequest(); //400         
                var count = _departmentRepositories.Delete(department);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }



            }
            return View(department);

        }




    }


}
