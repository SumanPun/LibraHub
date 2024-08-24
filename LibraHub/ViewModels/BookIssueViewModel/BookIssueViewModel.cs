namespace LibraHub.ViewModels.BookIssueViewModel
{
    public class BookIssueViewModel
    {
        public int Id { get; set; }
        public DateTime IssueDate { get; set; }
        public string? StudentName { get; set; }
        public string? Book { get; set; }
        public string? Publication { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
