#nullable enable
using System;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.LevelDesign.Scripts;

namespace Daipan.Battle.scripts
{
    public sealed class WaveState
    {
        public int CurrentWave { get; private set; }
        readonly int _waveTotalCount;

        public WaveState(EnemyParamsManager enemyParamsManager)
        {
            _waveTotalCount = enemyParamsManager.enemyWaveParams.Count; 
        }

        public void NextWave()
        {
            if (CurrentWave + 1 < _waveTotalCount)
                CurrentWave++;
        }
    }
}