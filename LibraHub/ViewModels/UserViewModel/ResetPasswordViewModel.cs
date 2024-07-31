using System.ComponentModel.DataAnnotations;

namespace LibraHub.ViewModels.UserViewModel
{
    public class ResetPasswordViewModel
    {
        [Required]
        public string? Id { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? ConfirmPassword { get; set; }

        public string? EmployeeId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
    }
}
