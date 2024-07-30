using System.ComponentModel.DataAnnotations.Schema;

namespace LibraHub.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string? CreatedUserId { get; set; }
        [ForeignKey(nameof(CreatedUserId))]
        public ApplicationUser? CreatedUser { get; set; }
        public string? ModifiedUserId { get; set; }
        [ForeignKey(nameof(ModifiedUserId))]
        public ApplicationUser? ModifiedUser { get; set; }
        public string? ImageUrl { get; set; }
        public string? Grade { get; set; }

        [NotMapped]
        public string? FullName => $"{FirstName} {LastName}";
    }
}
