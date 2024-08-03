using System.ComponentModel.DataAnnotations;

namespace LibraHub.ViewModels.CategoryViewModel
{
    public class CreateCategoryViewModel
    {
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
