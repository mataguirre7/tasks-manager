using API.Definitions.Entities;
using API.Domain.Extended;
using API.Domain.Items;

namespace API.Domain.Comments
{
    public class Comment : Entity<Guid>
    {
        // Foreign keys
        public Guid TaskId { get; set; }
        public Guid AuthorId { get; set; }

        // Properties
        public string Content { get; set; }

        // Navigation properties
        public ApplicationUser Author { get; set; }
        public Item Item { get; set; }
    }
}