using API.Definitions.Entities;
using API.Domain.Columns;
using API.Domain.Comments;
using API.Domain.Extended;
using API.Shared.Items;

namespace API.Domain.Items
{
    public class Item : Entity<Guid>
    {
        // Properties
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public ItemStatus Status { get; set; } = ItemStatus.ToDo;
        public ICollection<Comment> Comments { get; set; }

        // Foreign keys
        public Guid ColumnId { get; set; }
        public Guid AssignedUserId { get; set; }

        // Navigation properties
        public ApplicationUser AssignedUser { get; set; }
        public Column Column { get; set; }
    }
}