using System;

namespace Millennium.Domain.Entities
{
    public abstract class AuditableEntity
    {
        public DateTime CreatedAt { get; protected set; }
        public DateTime? DeletedAt { get; protected set; }
        
        protected AuditableEntity()
        {
            CreatedAt = DateTime.UtcNow;
            DeletedAt = null;
        }
    }
}