namespace LibraHub.ViewModels.BookViewModel
{
    public class BookViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Author { get; set; }
        public int NumberOfCopies { get; set; }
        public int AvailableCopies { get; set; }
        public string? CategoryName { get; set; }
    }
}
