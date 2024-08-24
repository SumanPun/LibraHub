using System.ComponentModel.DataAnnotations;

namespace LibraHub.ViewModels.BookIssueViewModel
{
    public class CreateBookIssueHistoryViewModel
    {
        [Required]
        public int BookIssueId { get; set; }
        [Required]
        public string? Message { get; set; }
    }
}
