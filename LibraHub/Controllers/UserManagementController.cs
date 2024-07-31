using AspNetCoreHero.ToastNotification.Abstractions;
using LibraHub.Constants;
using LibraHub.Helpers.Interface;
using LibraHub.Models;
using LibraHub.ViewModels.UserViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace LibraHub.Controllers
{
    [Authorize(Roles =UserRoles.Admin)]
    public class UserManagementController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly INotyfService _notyfService;
        private readonly IFileHelper _fileHelper;
        public UserManagementController(UserManager<ApplicationUser> userManager,
                            INotyfService notyfService,
                            IFileHelper fileHelper)
        {
            _userManager = userManager;
            _notyfService = notyfService;
            _fileHelper = fileHelper;
        }


        public async Task<IActionResult> Index()
        {
            var users = await _userManager.GetUsersInRoleAsync(UserRoles.User);
            var result = users.Select(x => new UserViewModel()
            {
                Id = x.Id,
                EmployeeId = x.EmployeeId,
                UserName = x.UserName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                FirstName = x.FirstName,
                LastName = x.LastName,
                ImageUrl = x.ImgUrl,
                Status = x.Status,
            }).ToList();
            return View(result);
        }
        public IActionResult Create()
        {
            return View(new UserViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            try
            {
                using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
                var checkEmail = await _userManager.FindByEmailAsync(model.Email!);
                if(checkEmail != null)
                {
                    _notyfService.Error("Email already exists");
                    return View(model);
                }
                var checkUserName = await _userManager.FindByNameAsync(model.UserName!);
                if(checkUserName != null)
                {
                    _notyfService.Error("Username already exists");
                    return View(model);
                }
                var user = new ApplicationUser()
                {
                    FirstName = model.UserName,
                    LastName = model.LastName,
                    Email = model.Email,
                    UserName = model.UserName,
                    PhoneNumber = model.PhoneNumber,
                    Address = model.Address,
                    EmployeeId = "Emp-" + DateTime.Now.ToString("ddMMyyyyHHmmss"),
                    Status = true,
                    CreatedDate = DateTime.UtcNow,
                };

                if(model.Image != null)
                {
                    user.ImgUrl = await _fileHelper.UploadFile(model.Image, "user");
                }
                var result = await _userManager.CreateAsync(user, model.Password!);
                if(result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, UserRoles.User);
                }
                tx.Complete();
                _notyfService.Success("User Created Successfully");
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                _notyfService.Error(ex.Message);
                return View(model);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                _notyfService.Error("User not found");
                return RedirectToAction(nameof(Index));
            }
            var data = new EditUserViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
            };
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if(!ModelState.IsValid) return View(model);
            try
            {
                var user = await _userManager.FindByIdAsync(model.Id!);
                if (user == null)
                {
                    _notyfService.Error("User not found");
                    return RedirectToAction(nameof(Index));
                }
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.PhoneNumber = model.PhoneNumber;
                user.Address = model.Address;
                if(model.Image != null)
                {
                    user.ImgUrl = await _fileHelper.UploadFile(model.Image, "user");
                }
                await _userManager.UpdateAsync(user);
                _notyfService.Success("User updated successfully");
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                _notyfService.Error(ex.Message);
                return View(model);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                _notyfService.Error("User not found");
                return RedirectToAction(nameof(Index));
            }
            var vm = new UserDetailsViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                ImageUrl = user.ImgUrl,
                CreatedDate = user.CreatedDate,
                EmployeeId = user.EmployeeId,
                Status = user.Status,
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    _notyfService.Error("User not found");
                    return RedirectToAction(nameof(Index));
                }
                await _userManager.DeleteAsync(user);
                _notyfService.Success("User deleted successfully");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleStatus(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    _notyfService.Error("User not found");
                    return RedirectToAction(nameof(Index));
                }
                user.Status = !user.Status;
                await _userManager.UpdateAsync(user);
                _notyfService.Success("User status updated successfully");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }
        [HttpGet]
        public async Task<IActionResult> ResetPassword(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                _notyfService.Error("User not found");
                return RedirectToAction(nameof(Index));
            }
            var vm = new ResetPasswordViewModel()
            {
                Id = user.Id,
                FullName = user.FullName,
                EmployeeId = user.EmployeeId,
                UserName = user.UserName,
                Email = user.Email,
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            try
            {
                var user = await _userManager.FindByIdAsync(model.Id!);
                if (user == null)
                {
                    _notyfService.Error("User not found");
                    return RedirectToAction(nameof(Index));
                }
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                await _userManager.ResetPasswordAsync(user, token, model.Password!);
                _notyfService.Success("Password reset successfully");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return View(model);
            }
        }
    }
}
