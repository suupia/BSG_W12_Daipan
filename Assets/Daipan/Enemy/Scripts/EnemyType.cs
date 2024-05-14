#nullable enable
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Daipan.Enemy.Scripts
{
    public enum EnemyType
    {
        None,
        W,
        A,
        S,
        Cheetah
    }

    public struct EnemyEnum
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsBoss { get; set; }

        public static EnemyEnum[] Values { get; }

        EnemyEnum(int id, string name, bool isBoss = false)
        {
            (Id, Name, IsBoss) = (id, name, isBoss);
        }

        static EnemyEnum()
        {
            Values = new[]
            {
                W,
                A,
                S,
                Cheetah
            };
        }

        public static EnemyEnum None = new(0, "None");
        public static EnemyEnum W = new(1, "W");
        public static EnemyEnum A = new(2, "A");
        public static EnemyEnum S = new(3, "S");
        public static EnemyEnum Cheetah = new(4, "Cheetah", true);

        #region Overrides

        public static bool operator ==(EnemyEnum a, EnemyEnum b)
        {
            return a.Id == b.Id && a.Name == b.Name;
        }

        public static bool operator !=(EnemyEnum a, EnemyEnum b)
        {
            return !(a == b);
        }

        public override bool Equals(object? obj)
        {
            if (obj is EnemyEnum enemyEnum) return this == enemyEnum;
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