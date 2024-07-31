using System.ComponentModel.DataAnnotations;

namespace LibraHub.ViewModels.UserViewModel
{
    public class CreateUserViewModel
    {
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        [Compare("Password")]
        public string? ConfirmPassword { get; set; }
        public string? Address { get; set; }
        public IFormFile? Image { get; set; }
    }
}
