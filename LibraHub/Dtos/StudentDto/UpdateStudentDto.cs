namespace LibraHub.Dtos.StudentDto
{
    public class UpdateStudentDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? ModifiedUserId { get; set; }
        public string? ImageUrl { get; set; }
        public string? Grade { get; set; }
    }
}
