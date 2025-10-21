using API.Definitions.Entities;
using API.Domain.Boards;
using API.Domain.Extended;

namespace API.Domain.Workspaces
{
    public class Workspace : Entity<Guid>
    {
        /* Foreign keys */
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Board> Boards { get; set; }
        public ICollection<ApplicationUser> WorkspaceMembers { get; set; } = new List<ApplicationUser>();
        /* Navigation properties */
        public ApplicationUser User { get; set; }
    }
}