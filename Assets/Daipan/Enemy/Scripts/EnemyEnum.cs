#nullable enable
namespace Daipan.Enemy.Scripts
{
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
                None,
                W,
                A,
                S,
                Boss,
            };
        }

        public static EnemyEnum None = new(0, "None");
        public static EnemyEnum W = new(1, "W");
        public static EnemyEnum A = new(2, "A");
        public static EnemyEnum S = new(3, "S");
        public static EnemyEnum Boss = new(4, "Boss", true);

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