namespace LibraHub.ViewModels.BookViewModel
{
    public class BookDetailViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Author { get; set; }
        public string? Publication { get; set; }
        public int NumberOfCopies { get; set; }
        public int AvailableCopies { get; set; }
        public string? Category { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
