using System.ComponentModel.DataAnnotations;

namespace LibraHub.ViewModels.CategoryViewModel
{
    public class EditCategoryViewModel
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
