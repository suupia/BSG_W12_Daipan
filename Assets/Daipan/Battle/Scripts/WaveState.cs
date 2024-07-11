#nullable enable
using System;
using Daipan.Enemy.Interfaces;

namespace Daipan.Battle.scripts
{
    public sealed class WaveState
    {
        // public int CurrentWave => _enemyWaveParamContainer.GetEnemyTimeLineParamDataIndex();
        public int CurrentWave => 0; // todo:temp

        readonly IEnemyWaveParamContainer _enemyWaveParamContainer;
        
        public WaveState(
            IEnemyWaveParamContainer enemyWaveParamContainer
            )
        {
            _enemyWaveParamContainer = enemyWaveParamContainer;
        }
    }

    public sealed class WaveStateNew
    {
        public int CurrentWave { get; private set; }
        public bool IsFinalWave;
        
        public WaveStateNew(IEnemyWaveParamContainer enemyWaveParamContainer)
        {
            // CurrentWave = enemyWaveParamContainer.GetEnemyTimeLineParamDataIndex();
            // IsFinalWave = CurrentWave == 0;
        }
    }
}