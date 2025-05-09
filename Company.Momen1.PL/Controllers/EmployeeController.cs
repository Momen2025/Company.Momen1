﻿using AutoMapper;
using Company.Momen1.BLL.Interfaces;
using Company.Momen1.DAL.Models;
using Company.Momen1.PL.DTO;
using Company.Momen1.PL.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Threading.Tasks;

namespace Company.Momen1.PL.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IEmployeeRepository _employeeRepository; 

        //private readonly IDepartmentRepository _departmentRepositories;
        private readonly IMapper _mapper;



        //ASk CLR Create object From EmployeeREpository
        public EmployeeController(
            //IEmployeeRepository employeeRepository,
            //IDepartmentRepository departmentRepositories,
            IUnitOfWork unitOfWork,
            IMapper mapper

            )
        {
            _unitOfWork = unitOfWork;
            //_employeeRepository = employeeRepository;
            //_departmentRepositories = departmentRepositories;
            _mapper = mapper;
        }
        [HttpGet]  //Get : /Department/Index
        public async Task<IActionResult> Index(string? SearchInput)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchInput))
            {
                employees = await _unitOfWork.EmployeeRepository.GetAllAsync();
            }
            else
            {
                employees = await _unitOfWork.EmployeeRepository.GetByNameAsync(SearchInput);


            }

            return View(employees);
        }

        [HttpGet]
        public async Task<IActionResult> Search(string? SearchInput)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchInput))
            {
                employees = await _unitOfWork.EmployeeRepository.GetAllAsync();
            }
            else
            {
                employees = await _unitOfWork.EmployeeRepository.GetByNameAsync(SearchInput);


            }

            return PartialView("EmployeePartialView/EmployeesTablePartialView", employees);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            ViewData["department"] = departments;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeDTO model)
        {
            if (ModelState.IsValid) //Server  Side  Vaildation
            {
                if (model.Image is not null)
                {
                    model.ImageName = DocumnentSettings.UploadFile(model.Image, "Image");
                }

                var employee = _mapper.Map<Employee>(model);
                await _unitOfWork.EmployeeRepository.AddASync(employee);

                var count = await _unitOfWork.CompleteAsync();

                if (count > 0)
                {
                    TempData["Message"] = "Employee is Create !";
                    return RedirectToAction(nameof(Index));
                }

            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? Id)
        {

            //var departments = _departmentRepositories.GetAll();
            ////ViewData["department"] = departments;
            //ViewBag.Departments = departments;


            if (Id is null) return BadRequest("Invalid ID "); //400
            var employee = await _unitOfWork.EmployeeRepository.GetAsync(Id.Value);
            if (employee is null) return NotFound(new { statusCode = 404, message = $"Employee With Id :{Id} is  not Found" });

            var dto = _mapper.Map<CreateEmployeeDTO>(employee);
            ViewBag.EmployeeId = Id;

            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? Id, string ViewName = "Edit")
        {
            if (Id is null) return BadRequest("Invalid id");
            var employee = await _unitOfWork.EmployeeRepository.GetAsync(Id.Value);

            var department = await _unitOfWork.DepartmentRepository.GetAllAsync();
            ViewData["department"] = department;


            if (employee is null) return NotFound(new { StatusCode = 404, Message = $"employee With Id : {Id} is not Found" });


            var dto = _mapper.Map<CreateEmployeeDTO>(employee);

            return View(ViewName, dto);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int Id, CreateEmployeeDTO model, string ViewName = "Edit")
        {
            if (ModelState.IsValid)
            {

                if (model.ImageName is not null && model.Image is not null)
                {

                    DocumnentSettings.DeleteFile(model.ImageName, "Image");
                }
                if (model.Image is not null)
                {
                    model.ImageName = DocumnentSettings.UploadFile(model.Image, "Image");
                }

                var employee = _mapper.Map<Employee>(model);
                employee.Id = Id;

                _unitOfWork.EmployeeRepository.Update(employee);
                var count = await _unitOfWork.CompleteAsync();
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }

            }

            return View(ViewName, model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? Id)
        {
            return await Edit(Id, "Delete");

        }
            [HttpPost]
            public async Task<IActionResult> Delete([FromRoute] int Id, CreateEmployeeDTO model)
            {
                if (ModelState.IsValid)
                {
                    var employee = _mapper.Map<Employee>(model);
                    employee.Id = Id;

                    _unitOfWork.EmployeeRepository.Delete(employee);
                    var count = await _unitOfWork.CompleteAsync();
                    if (count > 0)
                    {
                        if (model.ImageName is not null)
                        {
                            DocumnentSettings.DeleteFile(model.ImageName, "Image");
                        }
                        return RedirectToAction(nameof(Index));
                    }

                }
                return View(model);
            }

        }
    }

