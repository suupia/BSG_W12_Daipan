#nullable enable

using Daipan.Player.MonoScripts;

namespace Daipan.Battle.interfaces
{
    public interface IEnemyHp
    {
        int CurrentHp { get; }
        void DecreaseHp(EnemyDamageArgs enemyDamageArgs);
    }

    public record EnemyDamageArgs(int DamageValue, PlayerColor PlayerColor = PlayerColor.None);
}