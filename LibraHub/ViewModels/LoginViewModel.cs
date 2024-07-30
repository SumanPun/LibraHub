using System.ComponentModel.DataAnnotations;

namespace LibraHub.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username is required"), DataType(DataType.Text)]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Password is required"), DataType(DataType.Password),]
        public string? Password { get; set; }
        public bool IsRemember { get; set; }
    }
}
