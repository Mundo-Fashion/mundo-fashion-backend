using System;

namespace MundoFashion.Core
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; private set; }
        public bool Active { get; private set; }

        public Entity()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
            Active = true;
        }

        public virtual void Inativate()
        {
            Active = false;
        }
    }
}
