#nullable enable
using System;
using Daipan.Enemy.Scripts;

namespace Daipan.Battle.Scripts
{
    public record EnemyDamageArgs(int DamageValue, EnemyEnum EnemyEnum = EnemyEnum.None);
}