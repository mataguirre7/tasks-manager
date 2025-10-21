using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Definitions.Entities
{
    public class EntityDto
    {
        public DateTime? CreationTime { get; set; } = DateTime.UtcNow;
    }
}