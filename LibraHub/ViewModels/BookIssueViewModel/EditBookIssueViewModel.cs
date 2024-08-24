using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace LibraHub.ViewModels.BookIssueViewModel
{
    public class EditBookIssueViewModel
    {
        public int Id { get; set; }
        [Required]
        public int BookId { get; set; }
        [Required]
        public DateTime IssueDate { get; set; }
        [Required]
        public DateTime ReturnDate { get; set; }
        [Required]
        public int StudentId { get; set; }
        public string? Note { get; set; }

        public List<SelectListItem>? BookSelectList { get; set; }
        public List<SelectListItem>? StudentSelectList { get; set; }
    }
}
