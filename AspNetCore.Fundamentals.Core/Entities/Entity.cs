using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.Fundamentals.Core.Entities
{
    public abstract class Entity<TKey> : IEquatable<Entity<TKey>>
        where TKey : IEquatable<TKey>
    {
        public virtual TKey Id { get; protected set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Entity<TKey>);
        }

        public virtual bool Equals(Entity<TKey> other)
        {           
            if (ReferenceEquals(other, null))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (this.GetType() != other.GetType())
                return false;
            
            return EqualityComparer<TKey>.Default.Equals(this.Id, other.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
