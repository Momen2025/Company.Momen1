using Company.Momen1.DAL.Models;
using Company.Momen1.PL.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;

namespace Company.Momen1.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public UserManager<AppUser> UserManager { get; }


        #region SignUp

        [HttpGet] //GET
        public IActionResult SignUp()
        {
            return View();
        }

        // P@aaW0rd
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpDto model)
        {

            if (ModelState.IsValid)//Server Side VAlidation
            {
                var user =await _userManager.FindByNameAsync(model.UserName);
                if(user is null)
                {
                    user=  await _userManager.FindByEmailAsync(model.Email);
                    if(user is null)
                    {
                        //register
                         user = new AppUser()
                        {
                            UserName = model.UserName,
                            FirtName = model.FiestName,
                            LastName = model.LastName,
                            Email = model.Email,
                            IsAgree = model.IsAgrre,

                        };

                        var result = await _userManager.CreateAsync(user, model.Password);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("SignIn");
                        }

                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }

                }

                ModelState.AddModelError("", "InVAlid SignUp !!");

            }

            return View();
        }

        #endregion



        #region SignIn

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        //P@ssW0rd
        [HttpPost] //Account Logign
        public async Task<IActionResult> SignIn(SignInDto model)
        {
            if (ModelState.IsValid)
            {
               var user=await _userManager.FindByEmailAsync(model.Email);
                if(user is not null)
                {
                  var flag=await _userManager.CheckPasswordAsync(user,model.Password);
                    if (flag)
                    {
                        //signin
                       var result=await _signInManager.PasswordSignInAsync(user, model.Password, model.RemeberMe, false);
                        if (result.Succeeded)
                        {
                              return RedirectToAction(nameof(HomeController.Index),"Home");
                        }
                      
                    }
                }
                ModelState.AddModelError("","Invalid Login !");
            }


            return View(model);
        }
        #endregion


        #region SignOut
        [HttpGet]
        public new async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }


        #endregion



    }
}
