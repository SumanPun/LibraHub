namespace LibraHub.Dtos.BookDto
{
    public class CreateBookDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Author { get; set; }
        public string? Publication { get; set; }
        public int NumberOfCopies { get; set; }
        public int CategoryId { get; set; }
        public string? CreatedUserId { get; set; }
    }
}
