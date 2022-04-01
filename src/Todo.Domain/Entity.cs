using System;

namespace Todo.Domain
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }
        public DateTime ModifiedAt { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
    }
}
