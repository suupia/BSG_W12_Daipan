#nullable enable
using Daipan.LevelDesign.Enemy.Scripts;
using Daipan.Player.Scripts;

namespace Daipan.Battle.interfaces
{
    public interface IPlayerHp
    {
        int MaxHp { get; }
        int CurrentHp { get; }
        void SetHp(DamageArgs damageArgs); 
    }
    
    public record DamageArgs(int DamageValue, EnemyEnum enemyEnum = EnemyEnum.None);

}
