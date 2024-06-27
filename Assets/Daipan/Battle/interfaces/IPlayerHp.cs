#nullable enable
using System;
using Daipan.Enemy.Scripts;

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