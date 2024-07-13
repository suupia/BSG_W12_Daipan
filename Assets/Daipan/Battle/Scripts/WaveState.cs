#nullable enable
using System;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.LevelDesign.Scripts;

namespace Daipan.Battle.scripts
{
    public sealed class WaveState
    {
        public int CurrentWaveIndex { get; private set; }
        public int TotalWaveCount { get;}

        public WaveState(EnemyParamsManager enemyParamsManager)
        {
            TotalWaveCount = enemyParamsManager.enemyWaveParams.Count; 
        }

        public void NextWave()
        {
            if (CurrentWaveIndex + 1 < TotalWaveCount)
                CurrentWaveIndex++;
        }
    }
}