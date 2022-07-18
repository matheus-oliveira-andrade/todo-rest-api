using System;

namespace Todo.Domain.Entities
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }
        public DateTime ModifiedAt { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        protected Entity()
        {
        }

        protected Entity(Guid? id, DateTime? modifiedAt, DateTime? createdAt)
        {
            Id = id ?? Guid.NewGuid();
            CreatedAt = createdAt ?? DateTime.Now;
            ModifiedAt = modifiedAt ?? DateTime.Now;
        }
    }
}
