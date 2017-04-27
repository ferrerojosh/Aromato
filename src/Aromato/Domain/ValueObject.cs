using System;
using System.Linq;
using System.Reflection;

namespace Aromato.Domain
{
    /// <summary>
    /// An abstract class for identfying an object as a value object.
    /// </summary>
    /// <remarks>
    /// This implementation is based on Cesar De la Torre's implementation of value object which
    /// is licensed under the MS-LPL license.
    /// </remarks>
    /// <typeparam name="TValueObject">The value object type.</typeparam>
    public abstract class ValueObject<TValueObject> : IEquatable<TValueObject>
        where TValueObject : ValueObject<TValueObject>
    {

        public bool Equals(TValueObject other)
        {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;

            //compare all public properties
            var props = GetType().GetProperties();
            if (props != null && props.Any())
            {
                return props.All(p =>
                {
                    var left = p.GetValue(this, null);
                    var right = p.GetValue(other, null);

                    if (left is TValueObject)
                    {
                        return ReferenceEquals(left, right);
                    }
                    return left.Equals(right);
                });
            }
            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            var item = obj as ValueObject<TValueObject>;
            return item != null && Equals((TValueObject) item);
        }

        public override int GetHashCode()
        {
            var hashCode = 31;
            var changeMultiplier = false;
            const int index = 1;

            //compare all public properties
            var props = GetType().GetProperties();

            if (props == null || !props.Any()) return hashCode;
            foreach (var item in props)
            {
                var value = item.GetValue(this, null);
                if (value != null)
                {
                    hashCode = hashCode * ((changeMultiplier) ? 59 : 114) + value.GetHashCode();
                    changeMultiplier = !changeMultiplier;
                }
                else hashCode = hashCode ^ (index * 13);
            }
            return hashCode;
        }

        public static bool operator ==(ValueObject<TValueObject> left, ValueObject<TValueObject> right)
        {
            if (left == null) return (right == null);
            return left.Equals(right);
        }

        public static bool operator !=(ValueObject<TValueObject> left, ValueObject<TValueObject> right)
        {
            return !(left == right);
        }
    }
}