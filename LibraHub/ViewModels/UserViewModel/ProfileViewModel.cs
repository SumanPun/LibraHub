using System.ComponentModel.DataAnnotations;

namespace LibraHub.ViewModels.UserViewModel
{
    public class ProfileViewModel
    {
        public string? EmployeeId { get; set; }

        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? Address { get; set; }
        public string? ImageUrl { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        public IFormFile? Image { get; set; }
    }
}
