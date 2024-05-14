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

        EnemyEnum(int id, string name, bool isBoss = false) => 
            (Id, Name,IsBoss) = (id, name, isBoss);
        
        public static IEnumerable<EnemyEnum> GetAll() =>
            typeof(EnemyEnum).GetFields(BindingFlags.Public |
                                              BindingFlags.Static |
                                              BindingFlags.DeclaredOnly)
                .Where(field => field.FieldType == typeof(EnemyEnum))
                .Select(field => (EnemyEnum)field.GetValue(null));

        public static EnemyEnum None = new EnemyEnum(0, "None");
        public static EnemyEnum W = new EnemyEnum(1, "W");
        public static EnemyEnum A = new EnemyEnum(2, "A");
        public static EnemyEnum S = new EnemyEnum(3, "S");
        public static EnemyEnum Cheetah = new EnemyEnum(4, "Cheetah", true);

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
            if (obj is EnemyEnum enemyEnum)
            {
                return this == enemyEnum;
            } 
            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ Name.GetHashCode();
        }

        public override string ToString() => Name;

        #endregion


    }
}