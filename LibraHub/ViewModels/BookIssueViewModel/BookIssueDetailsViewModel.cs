namespace LibraHub.ViewModels.BookIssueViewModel
{
    public class BookIssueDetailsViewModel
    {
        public int Id { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string? Grade { get; set; }
        public string? Student { get; set; }
        public string? Book { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public string? Note { get; set; }
    }
}
