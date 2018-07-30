using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AspNetCore.Fundamentals.Core.Entities
{
    public abstract class ValueObject<T> : IEquatable<T>
        where T : ValueObject<T>
    {        

        public override bool Equals(object obj)
        {
            return Equals(obj as T);
        }

        public override int GetHashCode()
        {
            var properties = this.GetType().GetProperties();
            var fields = this.GetType().GetFields();
            int hashCode = 200;
            foreach (PropertyInfo property in properties)
            {
                if (!property.CanRead)
                    continue;

                var value = property.GetValue(this);
                if (value != null)
                    hashCode += (hashCode ^ 234568) + value.GetHashCode();
            }

            foreach (FieldInfo field in fields)
            {
                var value = field.GetValue(this);
                if (value != null)
                    hashCode += (hashCode ^ 1245) + value.GetHashCode();
            }

            return hashCode;
        }

        public virtual bool Equals(T other)
        {
            if (ReferenceEquals(other, null))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (this.GetType() != other.GetType())
                return false;

            return this.GetHashCode() == other.GetHashCode();
        }

        public static bool operator !=(ValueObject<T> x, ValueObject<T> y)
        {
            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
                return false;

            return !x.Equals(y);
        }

        public static bool operator ==(ValueObject<T> x, ValueObject<T> y)
        {
            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
                return false;

            return x.Equals(y);
        }
    }
}
