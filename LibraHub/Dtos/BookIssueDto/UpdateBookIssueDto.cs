namespace LibraHub.Dtos.BookIssueDto
{
    public class UpdateBookIssueDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int StudentId { get; set; }
        public string? Note { get; set; }
        public string? ModifiedUserId { get; set; }
    }
}
