using System;

namespace RandomUserCore.Models
{
     public abstract class EntityBase
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}