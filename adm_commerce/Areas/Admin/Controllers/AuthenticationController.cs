using adm_commerce.Areas.Admin.Models;
using adm_commerce.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace adm_commerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthenticationController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AuthenticationController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Login()
        {
            if (IsAuthenticated()) return RedirectToAction(nameof(Index), "Home");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AuthenticationLoginVM model)
        {
            if (IsAuthenticated())
                return RedirectToAction(nameof(Index), "Home");

            if (!ModelState.IsValid) return View(model);
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            if (!result.Succeeded)
            {
                model.ErrorMessage = "Email or password is wrong";
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            if (IsAuthenticated()) return RedirectToAction(nameof(Index), "Home");
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AuthenticationRegisterVM model)
        {
            if (IsAuthenticated())
                return RedirectToAction(nameof(Index), "Home");

            if (!ModelState.IsValid) return View(model);

            var existingUser = await _userManager.FindByEmailAsync(model.Email);

            if (existingUser != null)
            {
                model.ErrorMessage = "Email already exists";
            }
            var newUser = new AppUser
            {
                FullName = model.FullName,
                UserName = model.Email,
                Email = model.Email
            };
            var result = await _userManager.CreateAsync(newUser, model.Password);

            if (!result.Succeeded)
            {
                model.ErrorMessage = string.Join(" ,", result.Errors.Select(x => x.Description));
                return View(model);
            }

            return RedirectToAction(nameof(Login));
        }


        public async Task<IActionResult> Logout()
        {
            if (!IsAuthenticated())
                return RedirectToAction(nameof(Index), "Home");

            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        //public async Task<ActionResult> Temp()
        //{
        //    var user = await _userManager.FindByEmailAsync("bayim@mail.ru");

        //    await _userManager.AddToRoleAsync(user, "OrdinaryUser");
        //    return Ok();
        //}
        private bool IsAuthenticated()
        {
            return HttpContext.User.Identity.IsAuthenticated;
        }
    }

}

