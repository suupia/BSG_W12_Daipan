using Daipan.Enemy.Scripts;
using Daipan.LevelDesign.Enemy.Scripts;
using Daipan.Stream.Scripts;

namespace Daipan.Enemy.Interfaces
{
    public interface IEnemyWaveParamContainer
    {
        public int WaveTotalCount { get; }
        EnemyWaveParamData GetEnemyWaveParamData();
    }
}