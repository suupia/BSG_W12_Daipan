#nullable enable
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.LevelDesign.Scripts;
using Daipan.LevelDesign.Enemy.Scripts;

namespace Daipan.Enemy.Scripts
{
    public sealed class EnemyTimeLineParamData : IEnemyTimeLineParamData
    {
        readonly EnemyTimeLineParam _enemyTimeLineParam;

        public EnemyTimeLineParamData(EnemyTimeLineParam enemyTimeLineParam)
        {
            _enemyTimeLineParam = enemyTimeLineParam;
        }

        public double GetStartTime() => _enemyTimeLineParam.startTime;
        public double GetSpawnIntervalSec() => _enemyTimeLineParam.spawnIntervalSec;
        public double GetMoveSpeedRate() => _enemyTimeLineParam.moveSpeedRate;
        public double GetSpawnBossPercent() => _enemyTimeLineParam.spawnBossPercent;
        public double GetSpawnSpecialPercent() => _enemyTimeLineParam.spawnSpecialPercent;
    }
}