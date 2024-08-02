using System.ComponentModel.DataAnnotations;

namespace LibraHub.ViewModels.UserViewModel
{
    public class ChangePasswordViewModel
    {
        [Required]
        public string? OldPassword { get; set; }
        [Required]
        public string? NewPassword { get; set; }
        [Required]
        public string? ConfirmPassword { get; set; }

        public string? EmployeeId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
    }
}
