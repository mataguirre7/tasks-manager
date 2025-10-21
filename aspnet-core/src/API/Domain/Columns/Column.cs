using API.Definitions.Entities;
using API.Domain.Boards;
using API.Domain.Items;

namespace API.Domain.Columns
{
    public class Column : Entity<Guid>
    {
        public string Status { get; set; }
        public ICollection<Item> Items { get; set; }
        public Guid BoardId { get; set; }
        public Board Board { get; set; }
    }
}