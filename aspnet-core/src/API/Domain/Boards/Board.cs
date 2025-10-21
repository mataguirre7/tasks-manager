using API.Definitions.Entities;
using API.Domain.Columns;
using API.Domain.Extended;

namespace API.Domain.Boards
{
    public class Board : Entity<Guid>
    {
        // Foreign keys
        public Guid UserId { get; set; }

        // Properties
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Column> Columns { get; set; } = new List<Column>();

        // Navigation properties
        public ApplicationUser User { get; set; }
    }
}