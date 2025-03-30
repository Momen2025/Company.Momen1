using Company.Momen1.DAL.Models;
using Company.Momen1.PL.DTO;
using Company.Momen1.PL.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Company.Momen1.PL.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public UserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]  //Get : /Department/Index
        public async Task<IActionResult> Index(string? SearchInput)
        {
            IEnumerable<UserToReturnDto> users;
            if (string.IsNullOrEmpty(SearchInput))
            {
              users=  _userManager.Users.Select(U => new UserToReturnDto()
                {
                    Id=U.Id,
                    UserName=U.UserName,
                    Email=U.Email,
                    FirstName=U.FirtName,
                    LastName=U.LastName,
                    Roles=_userManager.GetRolesAsync(U).Result
                   
                });
            }
            else
            {
                users = _userManager.Users.Select(U => new UserToReturnDto()
                {
                    Id = U.Id,
                    UserName = U.UserName,
                    Email = U.Email,
                    FirstName = U.FirtName,
                    LastName = U.LastName,
                    Roles = _userManager.GetRolesAsync(U).Result

                }).Where(U=> U.FirstName.ToLower().Contains(SearchInput.ToLower()));


            }

            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string? Id, string viewName = "Details")
        {
            if (Id is null) return BadRequest("Invalid ID "); //400

            var user = await _userManager.FindByIdAsync(Id);
            if (user is null) return NotFound(new { statusCode = 404, message = $"User With Id :{Id} is  not Found" });

            var dto = new UserToReturnDto()
            {
                Id=user.Id,
                UserName=user.UserName,
                FirstName=user.FirtName,
                LastName=user.FirtName,
                Email=user.Email,
                Roles= _userManager.GetRolesAsync(user).Result
            };

            return View(viewName,dto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string? Id)
        {
            return await Details(Id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string Id, UserToReturnDto model)
        {
            if (ModelState.IsValid)
            {
                if (Id != model.Id) return BadRequest("Invalid Operations");

                var user=await _userManager.FindByIdAsync(Id);
                if (user is null) BadRequest("Invalid Operations");
                user.UserName = model.UserName;
                user.FirtName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;

              var result=await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View( model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string? Id)
        {
            return await Details(Id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string Id, UserToReturnDto model)
        {
            if (ModelState.IsValid)
            {
                if (Id != model.Id) return BadRequest("Invalid Operations");

                var user = await _userManager.FindByIdAsync(Id);
                if (user is null) BadRequest("Invalid Operations");


                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }

    }
}
