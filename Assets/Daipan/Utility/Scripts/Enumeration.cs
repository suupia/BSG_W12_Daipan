#nullable enable
using System;

namespace Daipan.Utility.Scripts
{
    public abstract class Enumeration : IComparable
    {
        public int Id { get; }
        public string Name { get; }
        
        protected Enumeration(int id, string name)
        {
            Id = id;
            Name = name;
        }

        #region Overrides
        public int CompareTo(object? other)
        {
            if (other == null) return 1;
            return Id.CompareTo(((Enumeration) other).Id);
        }

        public static bool operator ==(Enumeration a, Enumeration b)
        {
            return a.Id == b.Id && a.Name == b.Name;
        }

        public static bool operator !=(Enumeration a, Enumeration b)
        {
            return !(a == b);
        }

        public override bool Equals(object? obj)
        {
            if (obj is Enumeration enemyEnum) return this == enemyEnum;
            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ Name.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }

        #endregion
    }
}