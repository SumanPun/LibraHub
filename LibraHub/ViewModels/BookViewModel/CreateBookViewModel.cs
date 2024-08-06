using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace LibraHub.ViewModels.BookViewModel
{
    public class CreateBookViewModel
    {
        [Required]
        public string? Name { get; set; }
        public string? Author { get; set; }
        [Required]
        public string? Publication { get; set; }
        [Required]
        public int NumberOfCopies { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public string? CreatedUserId { get; set; }

        [ValidateNever]
        public List<SelectListItem>? Categories { get; set; }
    }
}
