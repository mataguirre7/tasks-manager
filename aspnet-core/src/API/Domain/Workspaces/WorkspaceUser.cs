using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Domain.Extended;

namespace API.Domain.Workspaces
{
    public class WorkspaceUser
    {
        public Guid WorkspaceId { get; set; }
        public Guid UserId { get; set; }

        // Navigation properties
        public Workspace Workspace { get; set; }
        public ApplicationUser User { get; set; }
    }
}