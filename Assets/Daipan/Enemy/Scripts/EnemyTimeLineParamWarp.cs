#nullable enable
using System;

namespace Daipan.Enemy.Scripts
{
    public class EnemyTimeLineParamWarp
    {
        public Func<double> GetStartTime { get; init; } = () => 0;
        public Func<double> GetSpawnIntervalSec { get; init; } = () => 1;
        public Func<double> GetMoveSpeedRate { get; init; } = () => 1;
        public Func<double> GetSpawnBossPercent { get; init; } = () => 10;
    }
}