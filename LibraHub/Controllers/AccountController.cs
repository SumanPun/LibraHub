using AspNetCoreHero.ToastNotification.Abstractions;
using LibraHub.Models;
using LibraHub.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraHub.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly INotyfService _notyfService;
        public AccountController(UserManager<ApplicationUser> userManager,
                                SignInManager<ApplicationUser> signInManager,
                                INotyfService notyfService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _notyfService = notyfService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (_signInManager.IsSignedIn(User)) return RedirectToAction("Index", "Home");
            return View(new LoginViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                if (!ModelState.IsValid) { return View(model); }

                var user = await _userManager.FindByNameAsync(model.UserName!);

                if (user == null)
                {
                    throw new Exception("User not found");
                }

                var checkPassword = await _userManager.CheckPasswordAsync(user, model.Password!);

                if (!checkPassword)
                {
                    throw new Exception("Password is incorrect");
                }

                if (!user.Status)
                {
                    throw new Exception("User is not active. Please contact admin");
                }

                var result = await _signInManager.PasswordSignInAsync(user, model.Password!, model.IsRemember, false);

                if (!result.Succeeded)
                {
                    throw new Exception("Login failed");
                }
                _notyfService.Success("Login successfully");
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return View(model);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _notyfService.Success("Logout Successfully");
            return RedirectToAction("Login");
        }
        
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
