namespace LibraHub.ViewModels.BookIssueViewModel
{
    public class BookIssueHistoryViewModel
    {
        public int Id { get; set; }
        public int BookIssueId { get; set; }
        public string? Message { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
