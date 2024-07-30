using System.ComponentModel.DataAnnotations.Schema;

namespace LibraHub.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Author { get; set; }
        public string? Publication { get; set; }
        public int NumberOfCopies { get; set; }
        public int AvailableCopies { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category? Category { get; set; }
        public string? CreatedUserId { get; set; }
        [ForeignKey(nameof(CreatedUserId))]
        public ApplicationUser? CreatedUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ModifiedUserId { get; set; }
        [ForeignKey(nameof(ModifiedUserId))]
        public ApplicationUser? ModifiedUser { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
