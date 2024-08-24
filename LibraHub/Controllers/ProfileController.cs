using AspNetCoreHero.ToastNotification.Abstractions;
using LibraHub.Helpers.Interface;
using LibraHub.Models;
using LibraHub.ViewModels.UserViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraHub.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly INotyfService _notyfService;
        private readonly IFileHelper _fileHelper;

        public ProfileController(UserManager<ApplicationUser> userManager, INotyfService notyfService, IFileHelper fileHelper)
        {
            _userManager = userManager;
            _notyfService = notyfService;
            _fileHelper = fileHelper;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                _notyfService.Error("User not found");
                return RedirectToAction("Index", "Home");
            }
            var vm = new ProfileViewModel()
            {
                EmployeeId = user.EmployeeId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                ImageUrl = user.ImgUrl,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                _notyfService.Error("User not found");
                return RedirectToAction("Index", "Home");
            }
            var vm = new ProfileViewModel()
            {
                EmployeeId = user.EmployeeId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                ImageUrl = user.ImgUrl,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProfileViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    _notyfService.Error("User not found");
                    return RedirectToAction("Index", "Home");
                }
                user.FirstName = vm.FirstName;
                user.LastName = vm.LastName;
                user.Address = vm.Address;

                if (vm.Image != null)
                {
                    user.ImgUrl = await _fileHelper.UploadFile(vm.Image, "user");
                }

                await _userManager.UpdateAsync(user);
                _notyfService.Success("Profile updated successfully");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return View(vm);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                _notyfService.Error("User not found");
                return RedirectToAction("Index", "Home");
            }
            var vm = new ChangePasswordViewModel
            {
                EmployeeId = user.EmployeeId,
                FullName = user.FullName,
                UserName = user.UserName,
                Email = user.Email,
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    _notyfService.Error("User not found");
                    return RedirectToAction("Index", "Home");
                }
                var verifyPassword = await _userManager.CheckPasswordAsync(user, vm.OldPassword!);
                if (!verifyPassword)
                {
                    _notyfService.Error("Old password is incorrect");
                    return View(vm);
                }
                await _userManager.ChangePasswordAsync(user, vm.OldPassword!, vm.NewPassword!);
                _notyfService.Success("Password changed successfully");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return View(vm);
            }
        }
    }
}
