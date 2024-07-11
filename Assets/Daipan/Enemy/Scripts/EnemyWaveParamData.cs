#nullable enable
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.LevelDesign.Scripts;
using Daipan.LevelDesign.Enemy.Scripts;

namespace Daipan.Enemy.Scripts
{
    public sealed class EnemyWaveParamData 
    {
        readonly EnemyWaveParam _enemyWaveParam;

        public EnemyWaveParamData(EnemyWaveParam enemyWaveParam)
        {
            _enemyWaveParam = enemyWaveParam;
        }

        public int GetSpawnEnemyCount() => _enemyWaveParam.spawnEnemyCount; 
        public double GetWaveIntervalSec() => _enemyWaveParam.waveIntervalSec;
        public double GetSpawnIntervalSec() => _enemyWaveParam.spawnIntervalSec;
        public double GetMoveSpeedRate() => _enemyWaveParam.moveSpeedRate;
        public double GetSpawnBossPercent() => _enemyWaveParam.spawnBossPercent;
        public double GetSpawnSpecialPercent() => _enemyWaveParam.spawnSpecialPercent;
        public double GetSpawnTotemPercent() => _enemyWaveParam.spawnTotemPercent;
    }
}