using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Definitions.Entities
{
    public class Entity<TKey> where TKey : struct
    {
        public TKey Id { get; set; } = default(TKey);
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;
    }
}