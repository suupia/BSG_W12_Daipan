#nullable enable
using System;

namespace Daipan.LevelDesign.Enemy.Scripts
{
    public class EnemyTimeLineParamData
    {
        public Func<double> GetStartTime { get; init; } = () => 0;
        public Func<double> GetSpawnDelaySec { get; init; } = () => 1;
        public Func<double> GetMoveSpeedRate { get; init; } = () => 1;
        public Func<double> GetSpawnBossPercent { get; init; } = () => 10;
    }
}