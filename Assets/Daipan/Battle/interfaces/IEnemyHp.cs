#nullable enable

using Daipan.Player.MonoScripts;

namespace Daipan.Battle.interfaces
{
    public interface IEnemyHp
    {
        int MaxHp { get; }
        int CurrentHp { get; }
        void DecreaseHp(EnemyDamageArgs enemyDamageArgs);
    }

    public record EnemyDamageArgs(int DamageValue, PlayerColor playerColor = PlayerColor.None);
}