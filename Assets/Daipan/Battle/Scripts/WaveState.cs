#nullable enable
using System;
using Daipan.Enemy.Interfaces;

namespace Daipan.Battle.scripts
{
    public sealed class WaveState
    {
        public int CurrentWave => _enemyWaveParamContainer.GetEnemyTimeLineParamDataIndex();

        readonly IEnemyWaveParamContainer _enemyWaveParamContainer;
        
        public WaveState(
            IEnemyWaveParamContainer enemyWaveParamContainer
            )
        {
            _enemyWaveParamContainer = enemyWaveParamContainer;
        }
    }
}