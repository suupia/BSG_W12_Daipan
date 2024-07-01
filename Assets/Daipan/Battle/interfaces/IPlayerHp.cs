#nullable enable
using System;
using Daipan.Enemy.Scripts;

namespace Daipan.Battle.interfaces
{
    public interface IPlayerHp
    {
        void SetHp(DamageArgs damageArgs);
    }

    public record DamageArgs(int DamageValue, EnemyEnum EnemyEnum = EnemyEnum.None);
}