using Company.Momen1.DAL.Models;
using Company.Momen1.PL.DTO;
using Company.Momen1.PL.Helper;
using Company.Momen1.PL.Views;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Company.Momen1.PL.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]  //Get : /Department/Index
        public async Task<IActionResult> Index(string? SearchInput)
        {
            IEnumerable<RoleToReturnDto> roles;
            if (string.IsNullOrEmpty(SearchInput))
            {
                roles = _roleManager.Roles.Select(U => new RoleToReturnDto()
                {
                    Id = U.Id,
                    Name = U.Name


                });
            }
            else
            {

                roles = _roleManager.Roles.Select(U => new RoleToReturnDto()
                {
                    Id = U.Id,
                    Name = U.Name


                }).Where(R => R.Name.ToLower().Contains(SearchInput.ToLower()));

            }

            return View(roles);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleToReturnDto model)
        {
            if (ModelState.IsValid) //Server  Side  Vaildation
            {

                var role = await _roleManager.FindByNameAsync(model.Name);
                if (role is null)
                {
                    role = new IdentityRole()
                    {
                        Name = model.Name
                    };
                    var result = await _roleManager.CreateAsync(role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }


                }


            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string? Id, string viewName = "Details")
        {
            if (Id is null) return BadRequest("Invalid ID "); //400

            var role = await _roleManager.FindByIdAsync(Id);
            if (role is null) return NotFound(new { statusCode = 404, message = $"Role With Id :{Id} is  not Found" });

            var dto = new RoleToReturnDto()
            {
                Id = role.Id,
                Name = role.Name
            };

            return View(viewName, dto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string? Id)
        {
            return await Details(Id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string Id, RoleToReturnDto model)
        {
            if (ModelState.IsValid)
            {
                if (Id != model.Id) return BadRequest("Invalid Operations");

                var role = await _roleManager.FindByIdAsync(Id);
                if (role is null) BadRequest("Invalid Operations");

                var roleresult = await _roleManager.FindByNameAsync(model.Name);
                if (roleresult is not null)
                {
                    role.Name = model.Name;

                    var result = await _roleManager.UpdateAsync(role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                }
                ModelState.AddModelError("", "Invaild Operations !!");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string? Id)
        {
            return await Details(Id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string Id, RoleToReturnDto model)
        {
            if (ModelState.IsValid)
            {
                if (Id != model.Id) return BadRequest("Invalid Operations");

                var role = await _roleManager.FindByIdAsync(Id);
                if (role is null) BadRequest("Invalid Operations");


                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Invalid Operations !!");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddOrRemoveUser(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role is null)
                return NotFound();

            ViewData["RoleId"] = roleId;

            var usersInRole = new List<UserInRoleViewModel>();

            var users = await _userManager.Users.ToListAsync();

            foreach (var user in users)
            {
                var userInRole = new UserInRoleViewModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName,


                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userInRole.IsSelected = true;
                }
                else
                {
                    userInRole.IsSelected = false;
                }
                usersInRole.Add(userInRole);
            }
            return View(usersInRole);

        }

        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUser(string roleId,List<UserInRoleViewModel> users)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role is null)
                return NotFound();

            if (ModelState.IsValid)
            {
                foreach (var user in users)
                {
                    var appuser = await _userManager.FindByIdAsync(user.UserId);
                    if (appuser is not null)
                    {

                        if (user.IsSelected && !await _userManager.IsInRoleAsync(appuser, role.Name))
                        {
                            await _userManager.AddToRoleAsync(appuser, role.Name);
                        }
                        else if (!user.IsSelected && await _userManager.IsInRoleAsync(appuser, role.Name))
                        {
                            await _userManager.RemoveFromRoleAsync(appuser, role.Name);
                        }
                    }

                    return RedirectToAction(nameof(Edit), new { Id = roleId });
                }
             
            }
            return View(users);
        }



    }
}
