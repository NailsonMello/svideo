using System;

namespace SVideo.Domain.Entities
{
    public abstract class GenericClass
    {
        protected GenericClass()
        { }

        public Guid Id { get; set; }

        public override bool Equals(object obj)
        {
            GenericClass compareTo = obj as GenericClass;

            if (ReferenceEquals(this, compareTo))
            {
                return true;
            }

            if (ReferenceEquals(null, compareTo))
            {
                return false;
            }

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(GenericClass a, GenericClass b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            {
                return true;
            }

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            {
                return false;
            }

            return a.Equals(b);
        }

        public static bool operator !=(GenericClass a, GenericClass b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907) + Id.GetHashCode();
        }

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}
