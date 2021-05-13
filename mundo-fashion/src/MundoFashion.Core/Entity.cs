using System;

namespace MundoFashion.Core
{
    public abstract class Entity
    {
        public Guid Id { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Entity()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }       
    }

    public abstract class ActivableEntity : Entity
    {
        public bool Active { get; private set; }
        public ActivableEntity()
        {
            Active = true;
        }

        public virtual void Inativate()
        {
            Active = false;
        }
    }
}
