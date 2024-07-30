using System.ComponentModel.DataAnnotations.Schema;

namespace LibraHub.Models
{
    public class BookIssueHistory
    {
        public int Id { get; set; }
        public int BookIssueId { get; set; }
        [ForeignKey(nameof(BookIssueId))]
        public BookIssue? BookIssue { get; set; }
        public string? Message { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
