#nullable enable
using Daipan.Utility.Scripts;

namespace Daipan.Enemy.Scripts
{
    public class EnemyEnum : Enumeration

    {
    public bool IsBoss { get; }

    public static EnemyEnum[] Values { get; }

    EnemyEnum(int id, string name, bool isBoss = false) : base(id, name)
    {
        IsBoss = isBoss; 
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
    
    }
}