using System.ComponentModel.DataAnnotations;

namespace LibraHub.ViewModels.UserViewModel
{
    public class EditUserViewModel
    {
        public string? Id { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public IFormFile? Image { get; set; }
    }
}
