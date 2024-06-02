#nullable enable
using System;

namespace Daipan.LevelDesign.Enemy.Scripts
{
    public class EnemyLevelDesignParamData
    {
        public required Func<int> spawnBossAmount { get; init; } = () => 10;
        public required Func<int> increaseIrritatedValueByBoss { get; init; } = () => 10;
        public required Func<int> currentKillAmount { get; init; } = () => 0;
    }
}