using System.ComponentModel.DataAnnotations.Schema;

namespace LibraHub.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Descrption { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string? CreatedUserId { get; set; }
        [ForeignKey(nameof(CreatedUserId))]
        public ApplicationUser? CreatedUser { get; set; }
        public string? ModifiedUserId { get; set; }
        [ForeignKey(nameof(ModifiedUserId))]
        public ApplicationUser? ModifiedUser { get; set; }
    }
}
